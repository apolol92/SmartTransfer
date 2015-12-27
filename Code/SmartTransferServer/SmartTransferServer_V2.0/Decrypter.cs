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
            RijndaelManaged rijManaged = GetRijndaelManaged(EncryptionKey);
            byte[] receivedData = Decrypt(encryptedRequest, rijManaged);
            string encryptedRequestStr = Encoding.Default.GetString(receivedData);

            encryptedRequestStr = encryptedRequestStr.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(encryptedRequestStr);
            try
            {
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        encryptedRequestStr = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                //Check if there the right password..
                return WRONG_PASSWORD;
                
            }
            //Remove password from string
            return encryptedRequestStr.Substring(SERVER_PW.Length,encryptedRequestStr.Length-SERVER_PW.Length);
        }
        private static RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        private static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        private static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
    }

}