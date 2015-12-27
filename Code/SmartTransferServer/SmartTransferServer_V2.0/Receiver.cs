using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public byte[] waitForCommand()
        {
            this.currentClient = this.ServerSocket.Accept();
            return receiveData();
        }

        public byte[] receiveData()
        {
            byte[] data = new byte[1024];
            int recv;
            int total_recv = 0;
            List<Byte[]> incoming = new List<Byte[]>();
            data = new byte[1024];
            bool stop = false;
            while (!stop)
            {
                recv = currentClient.Receive(data, 20, SocketFlags.None);
                total_recv += recv;
                byte[] data2 = new byte[recv];
                for (int i = 0; i < recv; i++)
                {
                    data2[i] = data[i];
                }
                incoming.Add(data2);

                Console.WriteLine(recv);
                data = new byte[1024];
                if (recv < 20)
                {
                    stop = true;
                }
            }
            byte[] total_data = new byte[total_recv];

            int pos = 0;
            for (int i = 0; i < incoming.Count; i++)
            {
                for (int d = 0; d < incoming[i].Length; d++)
                {
                    total_data[pos] = incoming[i][d];
                    
                    pos++;
                }
            }          
            return total_data;
        }

        
    }
}