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
            //Receive all data
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
                data = new byte[1024];
                //Logger.print(Encoding.Default.GetString(data));                
                if (recv < 20)
                {
                    stop = true;
                }
            }
            //Create total data array
            byte[] total_data = new byte[total_recv];
            //Add all bytes from incoming list to total_data
            int pos = 0;
            for (int i = 0; i < incoming.Count; i++)
            {
                for (int d = 0; d < incoming[i].Length; d++)
                {
                    total_data[pos] = incoming[i][d];
                    
                    pos++;
                }
            }          
            //Return total received data as byte array
            return total_data;
        }

        
    }
}