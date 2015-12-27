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
            RijndaelManaged rijManaged = GetRijndaelManaged(SERVER_PW);
            byte[] clearBytes = Encrypt(Encoding.Default.GetBytes(clearText), rijManaged);
            return Encoding.Default.GetString(clearBytes);
        }
        public static RijndaelManaged GetRijndaelManaged(String secretKey)
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

        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
    }
}