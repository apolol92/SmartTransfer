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

        public string decrypt(byte[] encryptedRequest)
        {
            string EncryptionKey = SERVER_PW;
            RijndaelManaged rijManaged = Crypto.GetRijndaelManaged(EncryptionKey);
            try {              
                byte[] receivedData = Crypto.Decrypt(encryptedRequest, rijManaged);          
                string encryptedRequestStr = Encoding.Default.GetString(receivedData);               
                encryptedRequestStr = encryptedRequestStr.Replace(" ", "+");
                return encryptedRequestStr.Substring(SERVER_PW.Length);
            }
            catch(Exception ex)
            {               
                return WRONG_PASSWORD;
            }          
        }
        
    }

}