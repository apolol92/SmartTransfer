using System;

namespace SmartTransferServer_V2._0
{
    internal class Killer
    {
        const int KEEP_ALIVE_TYP = 3;
        const long NOT_BORN = -1;
        const long LIVING_TIME = 30000;
        private long lastAlive;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public Killer()
        {
            this.lastAlive = NOT_BORN;
        }

        internal bool kill(Command requestCommand)
        {
            long currentTime = GetCurrentUnixTimestampMillis();
            if (Math.Abs(currentTime - this.lastAlive) > LIVING_TIME)
            {
                this.lastAlive = NOT_BORN;
                return true;
            }
            return false;
        }

        internal void clientEntreation(Command requestCommand)
        {
            if (requestCommand.Typ == KEEP_ALIVE_TYP)
            {
                if (this.lastAlive == NOT_BORN)
                {
                    this.lastAlive = GetCurrentUnixTimestampMillis();
                }
                else
                {
                    long currentTime = GetCurrentUnixTimestampMillis();
                    if (Math.Abs(currentTime - this.lastAlive) < LIVING_TIME)
                    {
                        this.lastAlive = currentTime;
                    }
                }
            }
        }
        private long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }
    }
}