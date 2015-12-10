using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;



namespace SmartTransferServer
{
    class Guardian
    {
        public const int NOBODY_GUARDED = -1;
        public const int NOT_NEEDED_BECAUSE_NO_LOGIN = 1;
        public const int ALIVE_TIME = 5;
        int guardedId;
        private string serverPassword = "test123";
        private long lastKeepAlive;

        public Guardian()
        {
            this.guardedId = NOBODY_GUARDED;
            this.lastKeepAlive = NOT_NEEDED_BECAUSE_NO_LOGIN;
        }

        public bool isGuarding()
        {
            return this.guardedId != NOBODY_GUARDED;
        }

        public int generateGuardingId()
        {
            //TODO: Should be a better method..
            Random rnd = new Random();
            return rnd.Next(1, 1000);
        }

        public void setGuardingId(int id)
        {
            this.guardedId = id;
        }

        public int getGuardingId()
        {
            return this.guardedId;
        }

        public string ServerPassword
        {
            get
            {
                return serverPassword;
            }

            set
            {
                serverPassword = value;
            }
        }

        public long LastKeepAlive
        {
            get
            {
                return lastKeepAlive;
            }

            set
            {
                lastKeepAlive = value;
            }
        }

        public bool isClientAlive()
        {
            long current_time = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            if (this.lastKeepAlive == NOT_NEEDED_BECAUSE_NO_LOGIN || Math.Abs(this.lastKeepAlive - current_time) <= ALIVE_TIME)
            {
                return true;
            }
            return false;
        }
        public bool keepClientAlive()
        {
            long current_time = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            if (this.lastKeepAlive==NOT_NEEDED_BECAUSE_NO_LOGIN || Math.Abs(this.lastKeepAlive - current_time) <= ALIVE_TIME)
            {
                this.lastKeepAlive = current_time;
                return true;
            }
            return false;
        }

        public void resetProtection()
        {
            this.guardedId = NOBODY_GUARDED;
            this.lastKeepAlive = NOT_NEEDED_BECAUSE_NO_LOGIN;
        }

        
    }
}
