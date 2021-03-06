﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SmartTransferServer
{
    class Executor
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
        Guardian guardian;
        public Executor(Guardian guardian)
        {
            this.guardian = guardian;
        }

        public Command execute(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            switch (cmd.Typ)
            {
                case GET_DATA_FROM_SERVER:
                    return getDataFromServer(cmd);
                case SAVE_DATA_ON_SERVER:
                    return saveDataOnServer(cmd);
                case DELETE_FILE_FROM_SERVER:
                    return deleteFileFromServer(cmd);
                case KEEP_ALIVE:
                    return keepALive(cmd);
                case GET_AVAIBLE_FILES:
                    return getAvaibleFiles(cmd);
                case CLIENT_LOGOUT:
                    return logout(cmd);
                case CLIENT_LOGIN:
                    return login(cmd);
                case CLIENT_WANT_THUMBNAIL:
                    return thumbnail(cmd);
                default:
                    return createErrorCommand(cmd);
            }
        }

        private Command thumbnail(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, SEND_CLIENT_THUMBNAIL, cmd.Filename, "will coming soon", "none");
        }

        private Command login(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, "none", "login success", "none");
        }

        private Command getDataFromServer(Command cmd)
        {
            string category = cmd.Parameter;
            string filename = cmd.Filename;
            FileManager MyFileManager = new FileManager();
            string data = MyFileManager.loadFile(category, filename);
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, SEND_DATA_TO_CLIENT, filename, category, data);
        }

        private Command saveDataOnServer(Command cmd)
        {
            string category = cmd.Parameter;
            string filename = cmd.Filename;
            FileManager MyFileManager = new FileManager();
            MyFileManager.saveFile(filename,cmd.Data);
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, filename, "saved file", "none");
        }

        private Command deleteFileFromServer(Command cmd)
        {
            string category = cmd.Parameter;
            string filename = cmd.Filename;
            FileManager MyFileManager = new FileManager();
            MyFileManager.deleteFile(filename);
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, filename, "deleted file", "none");
        }

        private Command keepALive(Command cmd)
        {
            this.guardian.keepClientAlive();
            //TODO!!
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, "none", "alive", "none");
        }

        private Command getAvaibleFiles(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            FileManager MyFileManager = new FileManager();
            XmlManager MyXmlManager = new XmlManager();
            List<String> allPaths = MyXmlManager.getAllChildsFrom((Categories)Enum.Parse(typeof(Categories), cmd.Parameter));
            List<String> allFiles = MyFileManager.listAllFilesInCategoryFolders(allPaths);
            return cmdFactory.createCommand(cmd.Id,SERVERNAME, SEND_AVAIBLE_FILES,"none",concatFiles(allFiles),"none");
        }

        public Command createErrorCommand(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, "none", "error", "none");
        }

        public string concatFiles(List<String> allFiles)
        {
            string files = "";
            for(int i = 0; i < allFiles.Count; i++)
            {
                //Replace '+', because some files have a name with a '+'
                files += allFiles[i].Replace("+",PLUS_CHAR) + "+";
            }
            //Remove last concatting '+'
            files = files.Substring(0, files.Length - 1);
            return files;
        }

        private Command logout(Command cmd)
        {
            this.guardian.setGuardingId(Guardian.NOBODY_GUARDED);
            CommandFactory cmdFactory = new CommandFactory();
            return cmdFactory.createCommand(cmd.Id, SERVERNAME, STATUS, "none", "logout", "none");
        }
    }

    
}
