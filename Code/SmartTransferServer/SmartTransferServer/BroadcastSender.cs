using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{
    class BroadcastSender
    {
        /// <summary>
        /// This is the port number of the BroadcastSender
        /// </summary>
        public const int PORT_NUMBER = 1337;
        /// <summary>
        /// We will send this message
        /// </summary>
        public const string I_AM_HERE = "I'm your SmartTransferServer";
        /// <summary>
        /// This his the UdpClient, which is used for sending
        /// </summary>
        UdpClient client;
        /// <summary>
        /// Broadcastaddresse
        /// </summary>
        IPEndPoint ip;
        /// <summary>
        /// Our byte message.. we will cast I_AM_HERE to bytes
        /// </summary>
        byte[] bytes;
        /// <summary>
        /// Sends every n_secs a broadcast
        /// </summary>
        int n_secs;
        /// <summary>
        /// BroadcastSender active?
        /// </summary>
        bool active;

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        /// <summary>
        /// Initiliaze the BroadcastSender
        /// </summary>
        public BroadcastSender(int n_secs)
        {
            this.client = new UdpClient();
            this.ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            this.bytes = Encoding.ASCII.GetBytes(I_AM_HERE);
            this.Active = true;
        }

        /// <summary>
        /// This method sends every 5 seconds a broadcast
        /// </summary>
        public void run()
        {
            while(this.active)
            {
                this.client.Send(this.bytes, this.bytes.Length, this.ip);
            }
        }

        /// <summary>
        /// Cleans the Sender..
        /// </summary>
        public void clean()
        {
            this.client.Close();
        }
    }
}
