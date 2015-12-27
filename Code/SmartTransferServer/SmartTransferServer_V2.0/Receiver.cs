using System;
using System.Collections.Generic;
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

        public byte[] waitForCommand()
        {
            this.currentClient = this.ServerSocket.Accept();
            return receiveData();
        }

        private byte[] receiveData()
        {
            //byte[] data = new byte[1024];
            //int recv;
            //string receivedData = "";
            //while (true)
            //{
            //    data = new byte[1024];
            //    recv = this.currentClient.Receive(data);
            //    if (recv == 0)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        receivedData += Encoding.ASCII.GetString(data, 0, recv);
            //    }
            //}
            //return receivedData;
            byte[] data = new byte[1024];
            int recv;
            int total_recv = 0;
            //data = new byte[1024];
            //recv = currentClient.Receive(data);
            //byte[] data2 = new byte[recv];
            //for(int i = 0; i < recv; i++)
            //{
            //    data2[i] = data[i];
            //}
            List<Byte[]> incoming = new List<Byte[]>();
            data = new byte[1024];
            bool stop = false;
            const int MAX = 20;
            while (!stop)
            {
                recv = this.currentClient.Receive(data, MAX, SocketFlags.None);
                total_recv += recv;
                byte[] data2 = new byte[recv];
                for (int i = 0; i < recv; i++)
                {
                    data2[i] = data[i];
                }
                incoming.Add(data2);
                Console.WriteLine(recv);
                data = new byte[1024];
                if (recv < MAX)
                {
                    stop = true;
                }
            }
            byte[] total_data = new byte[total_recv];

            int pos = 0;
            Console.WriteLine(total_recv);
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