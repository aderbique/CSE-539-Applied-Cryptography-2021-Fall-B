/* 
Author: austin@derbique.org
*/
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace P2
{
    class Program
    {
        // This function will help us get the input from the command line
        public static string GetInputFromCommandLine(string[] args)
        {
            // get the input from the command line
            string input = "";
            if (args.Length == 1)
            {
                input = args[0]; // Gets the first string after the 'dotnet run' command
            }
            else
            {
                Console.WriteLine("Not enough or too many inputs provided after 'dotnet run' ");
            }
            return input;
        }

        public static string CreateModifiedMD5(string input, string salt, int bytesLength)
        ///shamelessly stolen from https://stackoverflow.com/a/24031467
        /// Takes in an input string, salt, and length. Appends the salt to the input, 
        ///hashes, and then returns a string of bytes to the length defined by bytesLength
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] saltBytes = new byte[] {Convert.ToByte(salt,16)};
                byte[] saltedInputBytes = new byte[inputBytes.Length + saltBytes.Length];
                System.Buffer.BlockCopy(inputBytes, 0, saltedInputBytes, 0, inputBytes.Length);
                System.Buffer.BlockCopy(saltBytes, 0, saltedInputBytes, inputBytes.Length, saltBytes.Length);                                
                byte[] hashBytes = md5.ComputeHash(saltedInputBytes);
                byte[] reducedBytes = hashBytes.Take(bytesLength).ToArray();
                return BitConverter.ToString(reducedBytes);
            }
        }
        public static string GeneratePassword(string validChars, int length)
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }
            return res.ToString();
        }

        public static bool compareHashes(string hash1, string hash2, int bytesToCompare)
        {
            byte[] hashBytes1 = System.Text.Encoding.ASCII.GetBytes(hash1);
            byte[] hashBytes2 = System.Text.Encoding.ASCII.GetBytes(hash2);
            for (int i = 0; i < bytesToCompare; i++)
            {
                if (hashBytes1[i] != (hashBytes2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        
        public static string P2(string[] args)
        {
            string salt = args[0];
            const string validChars = "abcdefghijqlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 10;
            int bytesLength = 5;

/*          //used for testing
            string s = "C5";
            //string p1 = "AQJCMW0DGL";
            string p1 = "Hello World!";
            string hs = CreateModifiedMD5(p1, s, 5);
            Console.WriteLine(hs);
            return "something";

*/          
            Dictionary<string, string> hashTable = new Dictionary<string, string>();            
            while (true)
            {
                string password = GeneratePassword(validChars, length);
                string hash     = CreateModifiedMD5(password, salt, bytesLength);
                if (!hashTable.ContainsKey(hash))
                {
                    hashTable.Add(hash, password);
                }
                else
                {  
                    //check to make sure it's not the same password twice
                    if (password != hashTable[hash])
                    {
                    Console.WriteLine(password + "," + hashTable[hash]);
                    return password + "," + hashTable[hash];
                    }
                }
            } 
            
            // Some helpful hints:
            // The main idea is to concateneate the salt to a random string, 
            // then feed that into the hashFunction, 
            // then keep track of those salted hashes until you find a matching pair of salted hashes, 
            // then print the solution which is the two strings that gave the matching salted hashes
            // NOTE: When I say salted hashes, I mean that you salted the password and then fed it into the hashFunction. So it is the hash of the password+salt (in this case "+" means concatenated together into one)

            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netcore-3.1
            // hint: what does Create() do?

            // optional hint: review converting a string into a byte array (byte[]) and the reverse, converting a byte array (byte[]) into a string BitConverter.ToString(exampleByteArray).Replace("-", " ");

            // The next two lines are an example of code that can convert a string to a byte array
            // string example = "Edward Snowden";
            // byte[] exampleByteArray = Encoding.UTF8.GetBytes(example);

            // passwords have to be made only using alphanumeric characters, so you can make random passwords using any of the characters in the string provided below (note: The starter code doesn't include lowercase just for simplicity but you can include lowercase as well. )
            // string alphanumeric_characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // optional hint: What data structure can you use to store the salted hashes that has a really fast lookup time of O(1) (constant) ?
            // You don't have to use this data structure, but it will make your code run fast. The System.Collections.Generic libary is a good place to start

            // TODO: Employ the Birthday Paradox to find a collision in the MD5 hash function

            // These were given as en example, you are going to have to find two passwords that have matching salted hashes with your code and then output them for the autograder to see
            //string password1 = "AQJCMW0DGL";
            //string password2 = "I95ORWB1A7";
            //string P2_answer = password1 + "," + password2;
            //Console.WriteLine(P2_answer); // you can still print things to the console. The autograder will ignore this, it will only test the return value of this function
            
            // return the solution to the autograder
            //return P2_answer; // autograder will grade this value to see if it is correct
        }

        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P2(args); // This will run your project code. The autograder will grade the return value of the P1_2 function
        }

    }
}
