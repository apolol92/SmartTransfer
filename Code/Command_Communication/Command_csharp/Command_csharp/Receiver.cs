using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Command_csharp
{
    class Receiver
    {
        private Socket serverSocket;
        public Socket currentClient;
        private readonly int SERVER_PORT = 2210;
        private readonly int PACKET_SIZE = 20;

        public Receiver()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, SERVER_PORT);
            this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(ipep);
            this.serverSocket.Listen(10);
        }

        public byte[] waitForData()
        {
            this.currentClient = this.serverSocket.Accept();
            return receiveData();
        }

        public byte[] receiveData()
        {
            byte[] data = new byte[1024];
            int recv;
            int total_recv = 0;
            List<Byte[]> incoming = new List<Byte[]>();
            data = new byte[PACKET_SIZE];
            bool stop = false;
            //Receive all data
            while (!stop)
            {
                recv = this.currentClient.Receive(data, PACKET_SIZE, SocketFlags.None);


                total_recv += recv;
                byte[] data2 = new byte[recv];
                for (int i = 0; i < recv; i++)
                {
                    data2[i] = data[i];
                }
                incoming.Add(data2);
                data = new byte[PACKET_SIZE];
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

        public void closeAll()
        {
            this.serverSocket.Close();
            this.currentClient.Close();
        }


    }
}
