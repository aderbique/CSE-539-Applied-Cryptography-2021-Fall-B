using System;
using System.Security.Cryptography; // Aes
using System.Numerics; // BigInteger
using System.IO;

namespace P3
{
    class Program
    {

        public static string P3(string[] args)
        {
            // Make sure you are familiar with the System.Numerics.BigInteger class and how to use some of the functions it has (Parse, Pow, ModPow, Subtract, ToByteArray, etc.)
            
            // optional hint: for encryptiong/ decryption with AES, use google or another search engine to find the microsoft documentation on Aes (google this--> System.Security.Cryptography.Aes)
            // you can copy and paste the example code from microsoft documentation if you want as well. https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0

            // optional hint: here is an example of how to convert the IV input string to a byte array https://gist.github.com/GiveThanksAlways/df9e0fa9e7ea04d51744df6a325f7530

            // you will be using BigInteger functions for almost all, if not all mathmatical operations. (Pow, ModPow, Subtract)
            // N = 2^(N_e) - N_c (this calculation needs to be done using BigInteger.Pow and BigInteger.Subtract)

            // Diffie-Hellman key is g^(xy) mod N. In the input you are given g_y which is g^y. So to make the key you need to perform g_y^(x) using the BigInteger class
            // key = g_y^(x) mod N (this calculation needs to be done using BigInteger.ModPow)

            // you can convert a BigInteger into a byte array using the BigInteger.ToByteArray() function/method

            Console.WriteLine("just printing the input");
            foreach(var item in args)
            {
                Console.WriteLine(item); //DELETE, just a placeholder
            }
            
            /*

            dotnet run "A2 2D 93 61 7F DC 0D 8E C6 3E A7 74 51 1B 24 B2" 251 465 255 1311 2101864342 8995936589171851885163650660432521853327227178155593274584417851704581358902 "F2 2C 95 FC 6B 98 BE 40 AE AD 9C 07 20 3B B3 9F F8 2F 6D 2D 69 D6 5D 40 0A 75 45 80 45 F2 DE C8 6E C0 FF 33 A4 97 8A AF 4A CD 6E 50 86 AA 3E DF" AfYw7Z6RzU9ZaGUloPhH3QpfA1AXWxnCGAXAwk3f6MoTx

            */
            string P3_answer = "Edward Snowden"; 
            return P3_answer;

        }
        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P3(args); // This will run your project code. The autograder will grade the return value of the P1_2 function
        }
    }
}
