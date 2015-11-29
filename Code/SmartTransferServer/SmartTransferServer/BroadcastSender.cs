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
        /// Initiliaze the BroadcastSender
        /// </summary>
        public BroadcastSender()
        {
            this.client = new UdpClient();
            this.ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            this.bytes = Encoding.ASCII.GetBytes(I_AM_HERE);
        }

        /// <summary>
        /// This method sends the broadcast
        /// </summary>
        public void send()
        {
            this.client.Send(this.bytes, this.bytes.Length, this.ip);
            this.client.Close();
        }
    }
}
