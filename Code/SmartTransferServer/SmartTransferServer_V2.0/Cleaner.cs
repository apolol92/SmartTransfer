using System;
using System.Net.Sockets;

namespace SmartTransferServer_V2._0
{
    internal class Cleaner
    {
        public Cleaner()
        {
        }

        internal void clean(Socket currentClient)
        {
            try {
                currentClient.Close();
            }
            catch(Exception ex)
            {

            }
        }
    }
}