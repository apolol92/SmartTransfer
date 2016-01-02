using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SmartTransferServer_V2._0
{
    public class Decrypter
    {
        public static readonly string WRONG_PASSWORD="wrong password";
        public readonly string SERVER_PW;

        public Decrypter(string server_pw)
        {
            SERVER_PW = server_pw;
        }

        public byte[] decrypt(byte[] encryptedRequest)
        {
            string EncryptionKey = SERVER_PW;
            RijndaelManaged rijManaged = Crypto.GetRijndaelManaged(EncryptionKey);
            try {              
                byte[] receivedData = Crypto.Decrypt(encryptedRequest, rijManaged);                   
                return receivedData;
            }
            catch(Exception ex)
            {               
                return null;
            }          
        }
        
    }

}