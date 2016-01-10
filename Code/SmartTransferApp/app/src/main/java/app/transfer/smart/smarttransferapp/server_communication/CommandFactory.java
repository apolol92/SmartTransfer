package app.transfer.smart.smarttransferapp.server_communication;

import android.util.Base64;

import java.io.UnsupportedEncodingException;


/**
 * Created by apolol92 on 02.01.2016.
 */
public class CommandFactory {
    static final String markClamp = "oierkjvooejcoa";
    static final String markBackClamp = "aocjeoovjkreio";
    static final String markSemi = "phgjgpnopqbbv";
    static final String markNewLine = "adsfdsgasdg";
    static final String markTab = "orinbfdobiioeqnc";

    public static Command extractCommand(byte[] rawCmd)
    {
        int startClam = -1,endClam=-1;
        String header = "";
        for(int i = 0; i < rawCmd.length; i++)
        {

            if(rawCmd[i] == '{')
            {
                startClam = i;
                continue;
            }
            else if(rawCmd[i]=='}')
            {
                endClam = i;
                break;
            }
            header += (char)rawCmd[i];
        }
        try {

            header =new String(Base64.decode(header.getBytes(),Base64.DEFAULT),"UTF-8");
            header = "{" + header + "}";
            Command cmd = extractHeader(header);
            cmd.Data = extractData(rawCmd, endClam);
            return cmd;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        return null;
    }

    private static byte[] extractData(byte[] rawCmd, int endClam)
    {
        byte[] data = new byte[rawCmd.length - endClam-1];
        for(int i = endClam+1,d=0; i < rawCmd.length; i++,d++)
        {
            data[d] = rawCmd[i];
        }
        return data;
    }
    public static Command extractHeader(String strCommand)
    {
        String splitsCharacters = ";"+"|"+"\\{"+"|"+"\\}";
        Command nCommand = new Command();
        String[] commandParts = strCommand.split(splitsCharacters);
        if (commandParts == null)
        {
            return null;
        }

        for (int i = 0; i < commandParts.length; i++)
        {
            try
            {
                switch (i)
                {
                    case 0:
                        //ID
                        nCommand.Id = Integer.parseInt(commandParts[i + 1]);
                        break;
                    case 1:
                        //USERNAME
                        nCommand.Username = commandParts[i + 1];
                        nCommand.Username = unmarkSpecialCharacters(nCommand.Username);
                        break;
                    case 2:
                        //TYP
                        nCommand.Typ = Integer.parseInt(commandParts[i + 1]);
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

    public static Command createCommand(int id, String username, int typ ,String filename,String parameter, byte[] data)
    {
        Command cmd = new Command();
        cmd.Id = id;
        System.out.println( cmd.Id);
        cmd.Username = username;
        cmd.Typ = typ;
        cmd.Filename = filename;
        cmd.Parameter = parameter;
        cmd.Data = data;
        cmd = markAllSpecialCharacters(cmd);
        return cmd;
    }

    private static String unmarkSpecialCharacters(String str)
    {
        str = str.replace(markClamp, "{");
        str = str.replace(markBackClamp, "}");
        str = str.replace(markSemi, ";");
        str = str.replace(markNewLine, "\n");
        str = str.replace(markTab, "\t");
        return str;
    }

    public static Command markAllSpecialCharacters(Command cmd)
    {
        cmd.Username = markSpecialCharacters(cmd.Username);
        cmd.Filename = markSpecialCharacters(cmd.Filename);
        cmd.Parameter = markSpecialCharacters(cmd.Parameter);
        return cmd;
    }

    private static String markSpecialCharacters(String str)
    {
        str = str.replace("{", markClamp);
        str = str.replace("}", markBackClamp);
        str = str.replace(";", markSemi);
        str = str.replace("\n", markNewLine);
        str = str.replace("\t", markTab);
        return str;
    }
}
