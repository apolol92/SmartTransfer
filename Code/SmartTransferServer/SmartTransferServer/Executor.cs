using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{
    class Executor
    {
        const int GET_DATA_FROM_SERVER = 0;
        const int SAVE_DATA_ON_SERVER = 1;
        const int DELETE_FILE_FROM_SERVER = 2;
        const int KEEP_ALIVE = 3;
        const int GET_AVAIBLE_FILES = 4;
        const int CLIENT_LOGOUT = 8;

        public Executor()
        {
            
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
                default:
                    return createErrorCommand(cmd);
            }
        }

        

        private Command getAvaibleFiles(Command cmd)
        {
            return null;
        }

        private Command getDataFromServer(Command cmd)
        {

            return null;
        }

        private Command saveDataOnServer(Command cmd)
        {
            return null;
        }

        private Command deleteFileFromServer(Command cmd)
        {
            throw null;
        }

        private Command keepALive(Command cmd)
        {
            throw null;
        }

        public Command createErrorCommand(Command cmd)
        {
            return null;
        }
    }

    
}
