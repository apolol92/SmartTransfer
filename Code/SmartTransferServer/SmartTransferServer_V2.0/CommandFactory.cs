using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0
{
    public class CommandFactory
    {
        const string markClamp = "oierkjvooejcoa";
        const string markBackClamp = "aocjeoovjkreio";
        const string markSemi = "phgjgpnopqbbv";
        const string markNewLine = "adsfdsgasdg";
        const string markTab = "orinbfdobiioeqnc";

        public static Command extractCommand(byte[] rawCmd)
        {
            int startClam = -1, endClam = -1;
            string header = "";
            for (int i = 0; i < rawCmd.Length; i++)
            {

                if (rawCmd[i] == '{')
                {
                    startClam = i;
                    continue;
                }
                else if (rawCmd[i] == '}')
                {
                    endClam = i;
                    break;
                }
                header += (char)rawCmd[i];
            }
            header = Encoding.UTF8.GetString(Convert.FromBase64String(header));
            header = "{" + header + "}";
            Command cmd = extractHeader(header);
            cmd.Data = extractData(rawCmd, endClam);
            return cmd;
        }

        public static Command createCommand(int id, string username, int typ, string filename, string parameter, byte[] data)
        {
            Command cmd = new Command();
            cmd.Id = id;
            cmd.Username = username;
            cmd.Typ = typ;
            cmd.Filename = filename;
            cmd.Parameter = parameter;
            cmd.Data = data;
            cmd = markAllSpecialCharacters(cmd);
            return cmd;
        }

        private static byte[] extractData(byte[] rawCmd, int endClam)
        {
            byte[] data = new byte[rawCmd.Length - endClam - 1];
            for (int i = endClam + 1, d = 0; i < rawCmd.Length; i++, d++)
            {
                data[d] = rawCmd[i];
            }
            return data;
        }

        private static Command extractHeader(string header)
        {
            char[] splitsCharacters = new char[] { ';', '{', '}' };
            Command nCommand = new Command();
            String[] commandParts = header.Split(splitsCharacters);
            if (commandParts == null)
            {
                return null;
            }

            for (int i = 0; i < commandParts.Length; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0:
                            //ID
                            nCommand.Id = Int32.Parse(commandParts[i + 1]);
                            break;
                        case 1:
                            //USERNAME
                            nCommand.Username = commandParts[i + 1];
                            nCommand.Username = unmarkSpecialCharacters(nCommand.Username);
                            break;
                        case 2:
                            //TYP
                            nCommand.Typ = Int32.Parse(commandParts[i + 1]);
                            break;
                        case 3:
                            //FILENAME
                            nCommand.Filename = commandParts[i + 1];
                            nCommand.Filename = unmarkSpecialCharacters(nCommand.Filename);
                            break;
                        case 4:
                            //PARAMETER
                            nCommand.Parameter = commandParts[i + 1];
                            nCommand.Parameter = unmarkSpecialCharacters(nCommand.Parameter);
                            break;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }


            return nCommand;
        }
        private static string unmarkSpecialCharacters(string str)
        {
            str = str.Replace(markClamp, "{");
            str = str.Replace(markBackClamp, "}");
            str = str.Replace(markSemi, ";");
            str = str.Replace(markNewLine, "\n");
            str = str.Replace(markTab, "\t");
            return str;
        }

        private static Command markAllSpecialCharacters(Command cmd)
        {
            cmd.Username = markSpecialCharacters(cmd.Username);
            cmd.Filename = markSpecialCharacters(cmd.Filename);
            cmd.Parameter = markSpecialCharacters(cmd.Parameter);
            return cmd;
        }

        internal Command createLoginSuceedCommand(Authenticator smartAuthenticator)
        {
            byte[] nothing = new byte[] { 10, 10, 10, 10 };
            Command nCommand = CommandFactory.createCommand(smartAuthenticator.generateNewId(), "SERVER", 7, "none", "Login successed", nothing);
            return nCommand;
        }

        private static string markSpecialCharacters(string str)
        {
            str = str.Replace("{", markClamp);
            str = str.Replace("}", markBackClamp);
            str = str.Replace(";", markSemi);
            str = str.Replace("\n", markNewLine);
            str = str.Replace("\t", markTab);
            return str;
        }
    }
}
