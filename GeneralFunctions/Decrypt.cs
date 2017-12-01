using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace General
{
    public class Decrypt
    {
        /// <summary>
        /// decrypts passed Text with Key
        /// </summary>
        /// <param name="sText">string that should be decrypted</param>
        /// <param name="sKey">key that will be used for decryption</param>
        /// <returns></returns>
        public static string DecryptString(string sText, string sKey)
        {
            try
            {
                byte[] byteText = Convert.FromBase64String(sText); // Text from Base64 string to byte array
                PasswordDeriveBytes pdbKey = new PasswordDeriveBytes(sKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });   // combines Key with salt initializationVector IV
                MemoryStream msMemoryStream = new MemoryStream();   // memoryStream to accept encrypted bytes
                Rijndael algorithm = Rijndael.Create(); // create encryption alorithm
                algorithm.Key = pdbKey.GetBytes(32);    // set 32 bytes of the pdbKey as algorithm Key
                algorithm.IV = pdbKey.GetBytes(16); // set 16 bytes of the pdbKey as algorithm IV
                CryptoStream csCryptoStream = new CryptoStream(msMemoryStream, algorithm.CreateDecryptor(), CryptoStreamMode.Write);    // create cryptoStream that will write output from encryptorAlgorithm to memoryStream
                csCryptoStream.Write(byteText, 0, byteText.Length); // encrypt Text
                csCryptoStream.Close(); // close cryptoStream
                byte[] byteResult = msMemoryStream.ToArray();   // get encrypted Data from memoryStream
                return System.Text.Encoding.Unicode.GetString(byteResult);  // return string of converted byte array 
            }
            catch (Exception ex) { return "Error: " + Convert.ToString(ex.Message); }
        }
    }
}
