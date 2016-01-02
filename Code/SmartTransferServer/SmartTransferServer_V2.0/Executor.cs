using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SmartTransferServer_V2._0
{
    public class Executor
    {
        const string SERVERNAME = "SERVER";
        const string PLUS_CHAR = "anerhponqwasdgnpoegnaobqb";
        const int GET_DATA_FROM_SERVER = 0;
        const int SAVE_DATA_ON_SERVER = 1;
        const int DELETE_FILE_FROM_SERVER = 2;
        const int KEEP_ALIVE = 3;
        const int GET_AVAIBLE_FILES = 4;
        const int SEND_AVAIBLE_FILES = 5;
        const int SEND_DATA_TO_CLIENT = 6;
        const int STATUS = 7;
        const int CLIENT_LOGOUT = 8;
        const int CLIENT_LOGIN = 9;
        const int CLIENT_WANT_THUMBNAIL = 10;
        const int SEND_CLIENT_THUMBNAIL = 11;
        Authenticator SmartAuthenticator;
        Killer SmartKiller;

        public Executor(Authenticator SmartAuthenticator, Killer SmartKiller)
        {
            this.SmartAuthenticator = SmartAuthenticator;
            this.SmartKiller = SmartKiller;
        }


        public Command execute(Command cmd)
        {
            Command responseCommand;
            switch (cmd.Typ)
            {
                case GET_DATA_FROM_SERVER:
                    Logger.getDataFromServer();
                    responseCommand =  getDataFromServer(cmd);
                    break;
                case SAVE_DATA_ON_SERVER:
                    Logger.saveDataOnServer(cmd);
                    responseCommand = saveDataOnServer(cmd);
                    break;
                case DELETE_FILE_FROM_SERVER:
                    Logger.deleteFileFromServer(cmd);
                    responseCommand = deleteFileFromServer(cmd);
                    break;             
                case GET_AVAIBLE_FILES:
                    Logger.getAvaibleFiles();
                    responseCommand = getAvaibleFiles(cmd);
                    break;
                case CLIENT_LOGOUT:
                    Logger.userLoggedOut();
                    return logout();
                case CLIENT_WANT_THUMBNAIL:
                    Logger.clientWantThumbnail(cmd);
                    responseCommand = thumbnail(cmd);
                    break;
                default:
                    Logger.undefinedError();
                    return createErrorCommand();
                    
            }
            responseCommand.Id = SmartAuthenticator.generateNewId();
            Logger.generatedNewId();
            return responseCommand;
        }

        private Command createErrorCommand()
        {            
            Command cmd = CommandFactory.createCommand(-1, SERVERNAME, STATUS, "none", "undefined error", new byte[1]);
            return cmd;
        }

        private Command thumbnail(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            Security SmartSecurity = new Security();
            XmlManager xmlManager = new XmlManager();
            List<string> allRootPaths = xmlManager.getAllChilds();
            Image image = Image.FromFile(cmd.Filename);
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            ImageConverter converter = new ImageConverter();
            byte[] imgArray = (byte[])converter.ConvertTo(thumb, typeof(byte[]));               
            return CommandFactory.createCommand(this.SmartAuthenticator.Id, SERVERNAME, SEND_CLIENT_THUMBNAIL, cmd.Filename, "none", imgArray);
        }

        private Command logout()
        {
            this.SmartKiller.hardKill();
            this.SmartAuthenticator.logout();            
            return CommandFactory.createCommand(-1, SERVERNAME, STATUS, "none", "OK", new byte[1]);
        }

        private Command getAvaibleFiles(Command cmd)
        {
  
            XmlManager xmlManager = new XmlManager();
            FileManager fileManager = new FileManager();            
            List<string> allFilePaths = fileManager.listAllFilesInCategoryFolders(xmlManager.getAllChilds());
            Logger.print(allFilePaths.Count+"");
            //allFilePaths = sortFilesByCreationTime(allFilePaths);
            return CommandFactory.createCommand(cmd.Id, SERVERNAME, SEND_AVAIBLE_FILES, "none", concatFiles(allFilePaths), new byte[1]);
        }

        private List<string> sortFilesByCreationTime(List<string> files)
        {
            for (int i = 0; i < files.Count- 1; i++)
            {
                int j = i + 1;

                while (j > 0)
                {
                    long tsFilesj_1 = GetCurrentUnixTimestampMillis(File.GetCreationTime(files[j - 1]));
                    long tsFilej = GetCurrentUnixTimestampMillis(File.GetCreationTime(files[j]));
                    if (tsFilesj_1>tsFilej)
                    {
                        string temp = files[j - 1];
                        files[j - 1] = files[j];
                        files[j] = temp;
                    }                   
                    j--;
                }
            }
            return files;
        }

        private Command deleteFileFromServer(Command cmd)
        {
            Security SmartSecurity = new Security();
            XmlManager xmlManager = new XmlManager();
            List<string> allRootPaths = xmlManager.getAllChilds();
            CommandFactory cmdFactory = new CommandFactory();
            //if (SmartSecurity.PathIsAllowed(allRootPaths,cmd.Filename))
            //{
            FileManager fileManager = new FileManager();
            fileManager.deleteFile(cmd.Filename);
            return CommandFactory.createCommand(SmartAuthenticator.Id,SERVERNAME,STATUS,cmd.Filename,"deleted file",new byte[1]);
            //}
            //return CommandFactory.createCommand(SmartAuthenticator.Id, SERVERNAME, STATUS, cmd.Filename, "not deleted file", new byte[1]);
        }

        private Command saveDataOnServer(Command cmd)
        {
            Security SmartSecurity = new Security();
            XmlManager xmlManager = new XmlManager();
            List<string> allRootPaths = xmlManager.getAllChilds();
            string category = cmd.Parameter;
            string filename = cmd.Filename;
            FileManager MyFileManager = new FileManager();
            CommandFactory cmdFactory = new CommandFactory();
            if (SmartSecurity.PathIsAllowed(allRootPaths, filename))
            {
                
                MyFileManager.saveFile(filename, cmd.Data);               
                return CommandFactory.createCommand(cmd.Id, SERVERNAME, STATUS, filename, "saved file", new byte[1]);
            }
            else
            {
                MyFileManager.saveFile(filename, cmd.Data);
                Logger.print("Path isnt allowed..");
                return CommandFactory.createCommand(cmd.Id, SERVERNAME, STATUS, filename, "saved file", new byte[1]);
            }
            cmd.Filename = filename;
            return CommandFactory.createCommand(cmd.Id, SERVERNAME, STATUS, filename, "cant save file", new byte[1]);
        }

        private Command getDataFromServer(Command cmd)
        {
            string category = cmd.Parameter;
            string filename = cmd.Filename;
            FileManager MyFileManager = new FileManager();
            byte[] data = MyFileManager.loadFile(category, filename);
            CommandFactory cmdFactory = new CommandFactory();
            return CommandFactory.createCommand(cmd.Id, SERVERNAME, SEND_DATA_TO_CLIENT, filename, category, data);
        }
        public string concatFiles(List<String> allFiles)
        {
            string files = "";
            for (int i = 0; i < allFiles.Count; i++)
            {
                //Replace '+', because some files have a name with a '+'
                files += allFiles[i].Replace("+", PLUS_CHAR) + "+";
                if (i==allFiles.Count-1)
                {                    
                    files = files.Substring(0, files.Length - 1);
                }
            }
            //Remove last concatting '+'
            
            return files;
        }
        public long GetCurrentUnixTimestampMillis(DateTime current)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(current - UnixEpoch).TotalMilliseconds;
        }
    }
}