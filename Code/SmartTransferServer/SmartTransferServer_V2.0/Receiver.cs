using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmartTransferServer_V2._0
{
    public class Receiver
    {
        private Socket serverSocket;
        private Socket currentClient;

        public Socket ServerSocket
        {
            get
            {
                return serverSocket;
            }

            set
            {
                serverSocket = value;
            }
        }

        public Socket CurrentClient
        {
            get
            {
                return currentClient;
            }

            set
            {
                currentClient = value;
            }
        }

        

        public Receiver()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, SmartTransferServer.SERVER_PORT);
            this.ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.ServerSocket.Bind(ipep);
            this.ServerSocket.Listen(10);
        }

        internal string waitForCommand()
        {
            this.currentClient.Accept();
            return receiveData();
        }

        private string receiveData()
        {
            byte[] data = new byte[1024];
            int recv;
            string receivedData = "";
            while (true)
            {
                data = new byte[1024];
                recv = this.currentClient.Receive(data);
                if (recv == 0)
                {
                    break;
                }
                else
                {
                    receivedData += Encoding.ASCII.GetString(data, 0, recv);
                }
            }
            return receivedData;
        }

        internal string waitForRequestCommand()
        {
            throw new NotImplementedException();
        }
    }
}