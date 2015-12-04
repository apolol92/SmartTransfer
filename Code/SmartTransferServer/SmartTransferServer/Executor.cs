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

        const int GET_AVAIBLE_FILES = 4;
        public Executor()
        {
            
        }

        public Command execute(Command cmd)
        {
            CommandFactory cmdFactory = new CommandFactory();
            switch (cmd.Id)
            {
                case GET_DATA_FROM_SERVER:
                    return getDataFromServer(cmd);
                case SAVE_DATA_ON_SERVER:
                    return saveDataOnServer(cmd);

                case GET_AVAIBLE_FILES:
                    return getAvaibleFiles(cmd);
                default:

                    break;
            }
            return null;
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
    }

    
}
