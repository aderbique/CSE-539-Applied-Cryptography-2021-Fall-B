/* 
Author: austin@derbique.org
This is published to GH for educational purposes only. Academic dishonesty will not be tolerated.
Permission to use this code for course credit is strictly PROHIBITED.
*/
using System;
using System.Security.Cryptography; // Aes
using System.Numerics; // BigInteger
using System.IO;

namespace P3
{
    class Program
    {
        static byte[] get_bytes_from_string(string input)
        //Taken from https://gist.github.com/GiveThanksAlways/df9e0fa9e7ea04d51744df6a325f7530
        {
            var input_split = input.Split(' ');
            byte[] inputBytes = new byte[input_split.Length];
            int i = 0;
            foreach (string item in input_split)
            {
                inputBytes.SetValue(Convert.ToByte(item, 16), i);
                i++;
            }
            return inputBytes;
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        //Pulled from https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        //Pulled from https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static string P3(string[] args)
        {
            byte[] IV = get_bytes_from_string(args[0]);
            int g_e = int.Parse(args[1]);
            int g_c = int.Parse(args[2]);
            int N_e = int.Parse(args[3]);
            int N_c = int.Parse(args[4]);
            int x = int.Parse(args[5]);
            BigInteger gy = BigInteger.Parse(args[6]);
            byte[] C = get_bytes_from_string(args[7]);
            string P = args[8];
            BigInteger N = BigInteger.Subtract(BigInteger.Pow(2, N_e), N_c);
            byte[] key = BigInteger.ModPow(gy,x, N).ToByteArray();
            String decryptedC = DecryptStringFromBytes_Aes(C, key, IV);
            String encryptedP = BitConverter.ToString(EncryptStringToBytes_Aes(P, key, IV));
            return decryptedC + "," + encryptedP.Replace("-", " ");
        }
        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P3(args);
        }
    }
}
