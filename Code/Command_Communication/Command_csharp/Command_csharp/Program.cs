using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Command_csharp
{
    class Sender
    {
        public static void send(byte[] data, Socket currentClient)
        {
            currentClient.Send(data, data.Length, SocketFlags.None);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Command cmd = CommandFactory.createCommand(12, "USER", 4, "test", "sdf", FileManager.readFile());
            //FileManager.writeFile(cmd.Data);
            //Command extracted = CommandFactory.extractCommand(cmd.toByteArr());
            //FileManager.writeFile(cmd.Data);
            Receiver rec = new Receiver();
            byte[] data = rec.waitForData();
            Command cmd = CommandFactory.extractCommand(data);
            FileManager.writeFile(cmd.Data);
            Command response = CommandFactory.createCommand(23, "dsaf", 124, "asdgasd", "dsaf", FileManager.readFile());
            Sender.send(response.toByteArr(),rec.currentClient);
            rec.closeAll();
            
        }
    }
}
