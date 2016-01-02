package app.transfer.smart.smarttransfer;

/**
 * Created by apolol92 on 23.12.2015.
 */
public class CommandFactory {
    final int COMMAND_PARTS = 6;

    final String markClamp = "oierkjvooejcoa";
    final String markBackClamp = "aocjeoovjkreio";
    final String markSemi = "phgjgpnopqbbv";
    final String markNewLine = "adsfdsgasdg";
    final String markTab = "orinbfdobiioeqnc";

    public Command extractCommandFromStr(String strCommand)
    {
        String splitsCharacters = ";"+"|"+"\\{"+"|"+"\\}";
        Command nCommand = new Command();
        System.out.println("This shouldnt be null: "+strCommand);
        String[] commandParts = strCommand.split(splitsCharacters);
        System.out.println("StrCommand"+strCommand);
        System.out.println("True:"+commandParts.length);
        if (commandParts == null)
        {
            return null;
        }

        for (int i = 0; i < commandParts.length; i++)
        {
            try
            {
                System.out.println(i);
                switch (i)
                {
                    case 0:
                        //ID
                        System.out.println(commandParts[i + 1]);
                        nCommand.id = Integer.parseInt(commandParts[i + 1]);
                        break;
                    case 1:
                        //USERNAME
                        nCommand.username = commandParts[i + 1];
                        nCommand.username = unmarkSpecialCharacters(nCommand.username);
                        break;
                    case 2:
                        //TYP
                        nCommand.typ = Integer.parseInt(commandParts[i + 1]);
                        break;
                    case 3:
                        //FILENAME
                        nCommand.filename = commandParts[i + 1];
                        nCommand.filename = unmarkSpecialCharacters(nCommand.filename);
                        break;
                    case 4:
                        //PARAMETER
                        nCommand.parameter = commandParts[i + 1];
                        nCommand.parameter = unmarkSpecialCharacters(nCommand.parameter);
                        break;
                    case 5:
                        //DATA
                        nCommand.data = commandParts[i + 1];
                        nCommand.data = unmarkSpecialCharacters(nCommand.data);
                        //nCommand.data = new String(Base64.decode(nCommand.data,Base64.DEFAULT));
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

    public Command createCommand(int id, String username, int typ, String filename, String parameter, String data)
    {
        Command nCommand = new Command();
        nCommand.id = id;
        nCommand.username = markSpecialCharacters(username);
        nCommand.typ = typ;
        nCommand.filename = markSpecialCharacters(filename);
        nCommand.parameter = markSpecialCharacters(parameter);
        nCommand.data = markSpecialCharacters(data);
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
        cmd.username = markSpecialCharacters(cmd.username);
        cmd.filename = markSpecialCharacters(cmd.filename);
        cmd.parameter = markSpecialCharacters(cmd.parameter);
        cmd.data = markSpecialCharacters(cmd.data);
        return cmd;
    }

    private String markSpecialCharacters(String str)
    {
        str = str.replace("{", markClamp);
        str = str.replace("}", markBackClamp);
        str = str.replace(";", markSemi);
        str = str.replace("\n", markNewLine);
        str = str.replace("\t", markTab);
        return str;
    }
}
