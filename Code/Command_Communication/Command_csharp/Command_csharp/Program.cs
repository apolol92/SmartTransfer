using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
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
            string key = "test123456789123";
            //Command cmd = CommandFactory.createCommand(12, "USER", 4, "test", "sdf", FileManager.readFile());
            //FileManager.writeFile(cmd.Data);
            //Command extracted = CommandFactory.extractCommand(cmd.toByteArr());
            //FileManager.writeFile(cmd.Data);
            Console.WriteLine("Create Crypto");
            RijndaelManaged rijndaelManaged = Crypto.GetRijndaelManaged(key);
            Receiver rec = new Receiver();
            Console.WriteLine("Wait for Data");
            byte[] data = rec.waitForData();
            Console.WriteLine("Decryption");
            data = Crypto.Decrypt(data, rijndaelManaged);
            Console.WriteLine("Extract Command");
            Command cmd = CommandFactory.extractCommand(data);
            Console.WriteLine("Save File");
            FileManager.writeFile(cmd.Data);
            Console.WriteLine("Create response");
            Command response = CommandFactory.createCommand(23, "dsaf", 124, "asdgasd", "dsaf", FileManager.readFile());
            Console.WriteLine("Sebd resonse");
            Sender.send(Crypto.Encrypt(response.toByteArr(), rijndaelManaged), rec.currentClient);
            rec.closeAll();
            
        }
    }
}
