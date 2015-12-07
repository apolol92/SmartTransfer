package app.transfer.smart.smarttransferapp;

/**
 * Created by apolol92 on 07.12.2015.
 */
public class CommandFactory {
    final int COMMAND_PARTS = 6;

    final String markClamp = "oierkjvooejcoa";
    final String markBackClamp = "aocjeoovjkreio";
    final String markSemi = "phgjgpnopqbbv";
    final String markNewLine = "adsfdsgasdg";
    final String markTab = "orinbfdobiioeqnc";


    /// <summary>
    /// Use this method to extract commands.
    /// Be careful by using this method.. if you want to send this extracted command.. you have to mark all characters..
    /// </summary>
    /// <param name="strCommand"></param>
    /// <returns></returns>
    public Command extractCommandFromStr(String strCommand)
    {
        //char[] splitsCharacters = new char[] { ';', '{', '}' };
        Command nCommand = new Command();
        String[] commandParts = strCommand.split("\\;|\\{|\\}");
        if(commandParts == null ||commandParts.length < COMMAND_PARTS || commandParts.length > COMMAND_PARTS)
        {
            return null;
        }
        try {
            for (int i = 0; i < commandParts.length; i++)
            {
                switch (i)
                {
                    case 0:
                        //ID
                        nCommand.Id = Integer.getInteger(commandParts[i].substring(1, commandParts.length));
                        break;
                    case 1:
                        //USERNAME
                        nCommand.Username = commandParts[i];
                        nCommand.Username = unmarkSpecialCharacters(nCommand.Username);
                        break;
                    case 2:
                        //TYP
                        nCommand.Typ = Integer.getInteger(commandParts[i]);
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
                        nCommand.Data = commandParts[i].substring(0, commandParts.length - 1);
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
    public Command createCommand(int id, String username, int typ, String filename, String parameter, String data)
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

    private String unmarkSpecialCharacters(String str)
    {
        str = str.replace(markClamp, "{");
        str = str.replace(markBackClamp, "}");
        str = str.replace(markSemi, ";");
        str = str.replace(markNewLine, "\n");
        str = str.replace(markTab, "\t");
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

    private String markSpecialCharacters(String str)
    {
        str = str.replace("{",markClamp);
        str = str.replace("}",markBackClamp);
        str = str.replace(";",markSemi);
        str = str.replace("\n",markNewLine);
        str = str.replace("\t",markTab);
        return str;
    }

}
