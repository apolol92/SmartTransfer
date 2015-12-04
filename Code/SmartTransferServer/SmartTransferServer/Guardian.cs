using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{
    class Guardian
    {
        public const int NOBODY_GUARDED = -1;
        int guardedId;

        public Guardian()
        {
            this.guardedId = NOBODY_GUARDED;
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



    }
}
