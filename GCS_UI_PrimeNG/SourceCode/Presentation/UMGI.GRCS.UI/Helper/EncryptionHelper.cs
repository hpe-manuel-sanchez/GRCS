using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UMGI.GRCS.UI.Helper
{
    public static class EncryptionUtility
    {
        private const string EncryptionKey = "1234567890!";
        
        /// <summary> 
        /// The salt value used to strengthen the encryption. 
        /// </summary> 
        private readonly static byte[] Salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        /// <summary> 
        /// Encrypts any string using the Rijndael algorithm. 
        /// </summary> 
        /// <param name="inputText">The string to encrypt.</param> 
        /// <returns>A Base64 encrypted string.</returns> 
        public static string Encrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();
            var plainText = Encoding.Unicode.GetBytes(inputText);
            var secretKey = new Rfc2898DeriveBytes(EncryptionKey, Salt);

            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary> 
        /// Decrypts a previously encrypted string. 
        /// </summary> 
        /// <param name="inputText">The encrypted string to decrypt.</param> 
        /// <returns>A decrypted string.</returns> 
        public static string Decrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();
            var encryptedData = Convert.FromBase64String(inputText);
            var secretKey = new  Rfc2898DeriveBytes(EncryptionKey, Salt);

            using (var decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream(encryptedData))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainText = new byte[encryptedData.Length];
                        var decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }
    }
}
