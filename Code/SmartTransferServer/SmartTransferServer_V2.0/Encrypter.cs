using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SmartTransferServer_V2._0
{
    public class Encrypter
    {
        public readonly string SERVER_PW;
        public Encrypter(string server_pw)
        {
            SERVER_PW = server_pw;
        }

        public string encrypt(Command responseCommand)
        {
            string clearText = SERVER_PW + responseCommand.toString();
            RijndaelManaged rijManaged = Crypto.GetRijndaelManaged(SERVER_PW);
            byte[] clearBytes = Crypto.Encrypt(Encoding.Default.GetBytes(clearText), rijManaged);
            return Encoding.Default.GetString(clearBytes);
        }
        
    }
}