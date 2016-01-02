using System;

namespace SmartTransferServer_V2._0
{
    public class Killer
    {
        const int KEEP_ALIVE_TYP = 3;
        const long NOT_BORN = -1;
        public const long LIVING_TIME = 8000;
        private long lastAlive;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Authenticator SmartAuthenticator;



        public Killer(Authenticator SmartAuthenticator)
        {
            this.LastAlive = NOT_BORN;
            this.SmartAuthenticator = SmartAuthenticator;
        }

        public bool kill(Command requestCommand)
        {
            if(requestCommand.Id==NOT_BORN)
            {
                return false;
            }
            long currentTime = GetCurrentUnixTimestampMillis();
            if (Math.Abs(currentTime - this.LastAlive) > LIVING_TIME)
            {
                this.LastAlive = NOT_BORN;
                return true;
            }
            return false;
        }

        public void hardKill()
        {
            this.lastAlive = NOT_BORN;
        }

        public int clientEntreation(Command requestCommand)
        {
            if (requestCommand.Typ == KEEP_ALIVE_TYP)
            {
                if (this.LastAlive == NOT_BORN)
                {
                    this.LastAlive = GetCurrentUnixTimestampMillis();                    
                }
                else
                {
                    long currentTime = GetCurrentUnixTimestampMillis();
                    if (Math.Abs(currentTime - this.LastAlive) < LIVING_TIME)
                    {
                        this.LastAlive = currentTime;
                    }
                }
                Logger.print("SURVIVED");
                return 1;

            }
            else
            {
                long currentTime = GetCurrentUnixTimestampMillis();
                if (Math.Abs(currentTime - this.LastAlive) > LIVING_TIME)
                {
                    this.LastAlive = NOT_BORN;
                    this.SmartAuthenticator.Login = false;
                }
            }
            return 0;
        }
        public long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public long LastAlive
        {
            get
            {
                return lastAlive;
            }

            set
            {
                lastAlive = value;
            }
        }

        
    }
}