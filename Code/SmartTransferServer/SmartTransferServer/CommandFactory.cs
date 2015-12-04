using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{
    class CommandFactory
    {
        const int COMMAND_PARTS = 6;

        const string markClamp = "oierkjvooejcoa";
        const string markBackClamp = "aocjeoovjkreio";
        const string markSemi = "phgjgpnopqbbv";
        const string markNewLine = "adsfdsgasdg";
        const string markTab = "orinbfdobiioeqnc";


        /// <summary>
        /// Use this method to extract commands.
        /// Be careful by using this method.. if you want to send this extracted command.. you have to mark all characters..
        /// </summary>
        /// <param name="strCommand"></param>
        /// <returns></returns>
        public Command extractCommandFromStr(String strCommand)
        {
            char[] splitsCharacters = new char[] { ';', '{', '}' };
            Command nCommand = new Command();
            String[] commandParts = strCommand.Split(splitsCharacters);
            if(commandParts == null ||commandParts.Length < COMMAND_PARTS || commandParts.Length > COMMAND_PARTS)
            {
                return null;
            }
            try {
                for (int i = 0; i < commandParts.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            //ID
                            nCommand.Id = Int32.Parse(commandParts[i].Substring(1, commandParts.Length));
                            break;
                        case 1:
                            //USERNAME
                            nCommand.Username = commandParts[i];
                            nCommand.Username = unmarkSpecialCharacters(nCommand.Username);
                            break;
                        case 2:
                            //TYP
                            nCommand.Typ = Int32.Parse(commandParts[i]);
                            break;
                        case 3:
                            //FILENAME
                            nCommand.Filename = commandParts[i];
                            nCommand.Filename = unmarkSpecialCharacters(nCommand.Filename);
                            break;
                        case 4:
                            //PARAMETER
                            nCommand.Parameter = commandParts[i];
                            nCommand.Parameter = unmarkSpecialCharacters(nCommand.Parameter);
                            break;
                        case 5:
                            //DATA
                            nCommand.Data = commandParts[i].Substring(0, commandParts.Length - 1);
                            nCommand.Data = unmarkSpecialCharacters(nCommand.Data);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return nCommand;
        }

        /// <summary>
        /// Use always this method to create a new command
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="typ"></param>
        /// <param name="filename"></param>
        /// <param name="parameter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Command createCommand(int id, string username, int typ, string filename, string parameter, string data)
        {
            Command nCommand = new Command();
            nCommand.Id = id;
            nCommand.Username = markSpecialCharacters(username);
            nCommand.Typ = typ;
            nCommand.Filename = markSpecialCharacters(filename);
            nCommand.Parameter = markSpecialCharacters(parameter);
            nCommand.Data = markSpecialCharacters(data);
            return nCommand;
        }

        private string unmarkSpecialCharacters(string str)
        {
            str = str.Replace(markClamp, "{");
            str = str.Replace(markBackClamp, "}");
            str = str.Replace(markSemi, ";");
            str = str.Replace(markNewLine, "\n");
            str = str.Replace(markTab, "\t");
            return str;
        }

        public Command markAllSpecialCharacters(Command cmd)
        {
            cmd.Username = markSpecialCharacters(cmd.Username);
            cmd.Filename = markSpecialCharacters(cmd.Filename);
            cmd.Parameter = markSpecialCharacters(cmd.Parameter);
            cmd.Data = markSpecialCharacters(cmd.Data);
            return cmd;
        }

        private string markSpecialCharacters(string str)
        {
            str = str.Replace("{",markClamp);
            str = str.Replace("}",markBackClamp);
            str = str.Replace(";",markSemi);
            str = str.Replace("\n",markNewLine);
            str = str.Replace("\t",markTab);
            return str;
        }
    }
}
