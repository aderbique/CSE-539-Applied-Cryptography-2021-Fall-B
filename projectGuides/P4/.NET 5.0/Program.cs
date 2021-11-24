/* 
Author: austin@derbique.org
This is published to GH for educational purposes only. Academic dishonesty will not be tolerated.
Permission to use this code for course credit is strictly PROHIBITED.
*/
using System;
using System.Numerics; // BigInteger
using System.Collections.Generic;

namespace P4
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

        public static BigInteger gcdExtendedBig(BigInteger a, BigInteger b)
        {
            if (b == 0){
                return(a);
            }

            BigInteger x1 = 0;
            BigInteger x2 = 1;
            BigInteger y1 = 1;
            BigInteger y2 = 0;
            BigInteger q, r, x, y, d;

            while (b > 0)
            {
                q = BigInteger.Divide(a,b);
                r = BigInteger.Subtract(a,BigInteger.Multiply(q,b));
                x = BigInteger.Subtract(x2, BigInteger.Multiply(q,x1));
                y = BigInteger.Subtract(y2, BigInteger.Multiply(q,y1));

                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            d = a;
            x = x2;
            y = y2;
            //Console.WriteLine("X is: " + x);
            //Console.WriteLine("Y is : " + y);
            return(y);
        }        

        public static string P4(string[] args)
        {
            /*
            * useful help for RSA encrypt/decrypt: https://www.di-mgt.com.au/rsa_alg.html
            * help with extended euclidean algorithm: https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
            * 
            */

            int p_e = int.Parse(args[0]);
            int p_c = int.Parse(args[1]);
            int q_e = int.Parse(args[2]);
            int q_c = int.Parse(args[3]);
            BigInteger CipherText = BigInteger.Parse(args[4]);
            BigInteger PlainText = BigInteger.Parse(args[5]);

            string CipherHex = CipherText.ToString("X");
            string PlainHex = PlainText.ToString("X");
            
            // RSA Alg taken from https://www.di-mgt.com.au/rsa_alg.html
            // Step 1
            BigInteger p = BigInteger.Subtract(BigInteger.Pow(2, p_e), p_c);
            BigInteger q = BigInteger.Subtract(BigInteger.Pow(2, q_e), q_c);

            // Step 2
            BigInteger n = BigInteger.Multiply(p,q);
            BigInteger phi = BigInteger.Multiply(BigInteger.Subtract(p,1),BigInteger.Subtract(q, 1));

            // Step 3
            int e = (int)BigInteger.Add(BigInteger.Pow(2,16), 1);
            BigInteger d = gcdExtendedBig(phi, e);
    
            //BigInteger test = BigInteger.ModPow(BigInteger.Multiply(e,d),1,phi);
            //Console.WriteLine("Checking the value of d = 1. d =" + test);

            // Step 5
            //BigInteger decrypted = BigInteger.ModPow(BigInteger.Pow(CipherText,d),1,n);
            BigInteger decrypted = BigInteger.ModPow(CipherText,d,n);

            //Console.WriteLine(decrypted);
            BigInteger encrypted = BigInteger.ModPow(BigInteger.Pow(PlainText,e),1,n);

            return decrypted + "," + encrypted;
        }

        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P4(args); // This will run your project code. The autograder will grade the return value of the P1_2 function
        }
    }
}
