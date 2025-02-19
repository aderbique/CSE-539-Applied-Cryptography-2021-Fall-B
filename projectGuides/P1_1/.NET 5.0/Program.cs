/*
Author: austin@derbique.org
This is published to GH for educational purposes only. Academic dishonesty will not be tolerated.
Permission to use this code for course credit is strictly PROHIBITED.
*/
using System;
using System.IO;
using System.Collections;

namespace P1_1
{
    class Program
    {
        // TODO: put your code in the solve function and have it return the solution in the form of a byte array
        public static byte[] Solve(string input, byte[] bmpBytes)
        {
            // Put your code in here
            // bitwise XOR function 0xFF ^ 0xAB
            // Look up BitArray in C# made from byte[]
            int counter = 26; //start at the 27th byte
            string[] bmpBytesStrArr = BitConverter.ToString(bmpBytes).Split("-");
            byte[] exampleByteArray = new byte[bmpBytes.Length]; // just a placeholder so that the code works from scatch without errors
            exampleByteArray = bmpBytes;
            string[] inputStrArr = input.Split(' ');
            foreach (string s in inputStrArr)
            {
                byte inputByte = Convert.ToByte(s,16);
                string bitString = Convert.ToString(inputByte, 2).PadLeft(8, '0');
                for (int i = 0; i < 8; i = i + 2)
                {
                    byte inputBitsByte = Convert.ToByte(Convert.ToString(bitString[i]) + Convert.ToString(bitString[i + 1]),2);
                    exampleByteArray[counter] = (byte)(inputBitsByte ^ bmpBytes[counter]);
                    counter += 1;
                }                
            }
            return exampleByteArray;
        }

        // This function will help us get the input from the command line
        public static string getInputFromCommandLine(string[] args)
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

        // You can use this function to save a .bmp file to help you debug/ see what is going on. This function is not needed but may be useful to help visualize things
        public static void MakeBMPFile(byte[] bmpBytes)
        {
            // This will get the current PROJECT directory (P1_1)
            string projectDirectory = System.IO.Directory.GetCurrentDirectory() + "/projectGuides/P1_1/.NET 5.0";
            System.IO.File.WriteAllBytes(projectDirectory + "/example.bmp", bmpBytes);
        }

        // The autograder will grade the return value of this function. You can use other helper functions but this function should take only the args as input and return the answer for the autograder to see
        public static string P1_1(string[] args)
        {
            // below is the example command of how you can run your program (or you can use the debugger)
            // dotnet run "B1 FF FF CC 98 80 09 EA 04 48 7E C9"

            // bmpBytes is defined in the instructions (I put it here to save you time)
            // Blue pixel = 0xFF,0x00,0x00
            // Red pixel = 0x00,0x00,0xFF
            // White pixel = 0xFF,0xFF,0xFF
            // Black pixel = 0x00,0x00,0x00
            // (Blue Green Red)
            byte[] bmpBytes = new byte[]
            {
                0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
                0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,0x18,0x00,
                0x00,0x00,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0xFF,
                0xFF,0xFF,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0x00,0x00,
                0xFF,0xFF,0xFF,
                0xFF,0x00,0x00,
                0xFF,0xFF,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00
            };

            string input = getInputFromCommandLine(args);
        
            // TODO: Convert input string to an array of bytes (inputBytes)
            Convert.ToByte("F8", 16); // This is an example of how to convert a string such as "F8" to a byte. (base 16 because F8 is Hexadecimal)

            byte[] inputBytes = new byte[10]; // this line is just a placeholder. You will need to start with the input string and convert the string to a byte array (in this example that byte array is named inputBytes)
            // TODO: put your code in the solve function and have it return the solution in the form of a byte array 
            byte[] solution = Solve(input, bmpBytes); 

            // format output
            string formatForAutograder = BitConverter.ToString(solution).Replace("-", " "); // This line formats the byte array into a string with spaces between the bytes instead of "-" between the bytes
            Console.WriteLine(formatForAutograder); // you can still print things to the console. The autograder will ignore this, it will only test the return value of this function

            return formatForAutograder; // autograder will grade this value to see if it is correct
        }

        // The Main function will run our program
        static void Main(string[] args)
        {   
            // args is the array that contains the command line inputs
            P1_1(args); // This will run your project code. The autograder will grade the return value of the P1_1 function
        }

    }
}
