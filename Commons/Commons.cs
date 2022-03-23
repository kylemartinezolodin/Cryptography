using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class CustomStringFunctions
    {
        /// <summary>
        /// Formats string with newline character ("\n") into what the system actually uses. This function is built because textbox does cannot use carriage return for some reason. The .NET textbox uses "\n" for newline, while text file from windows uses "\r\n". 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Return the string input except all the "\n" are changed into what the system (Non-Unix/Unix) uses for newline</returns>
        public static string stringEnvironmentEndline(string input) {
            return input.Replace("\n", Environment.NewLine);
        }
        /// <summary>
        /// This function is build to set a reliable .NET string to byte array encoding. This is one of the string to byte array conversion tested (ni kyle) to be reliable for cryptography
        /// </summary>
        /// <param name="input">input string to be converted to array of bytes</param>
        /// <returns>Converted array of bytes</returns>
        public static byte[] stringToDefaultByteArray(string input) {
            // NOTE!: DO NOT USE OTHER ENCODING OTHER THAN "Default", CERTAIN ENCODING HAVE VALUES THAT GETS CONVERTED WHEN PASSED IN ".NET's Windows Form Textbox"
            return Encoding.Default.GetBytes(input);
        }
        
        /// <summary>
        /// This function is build to set a reliable string to byte array conversion made by kyle. This is one of the string to byte array conversion tested (ni kyle) to be reliable for cryptography
        /// </summary>
        /// <param name="input">input string to be converted to array of bytes</param>
        /// <returns>Converted array of bytes</returns>
        public static byte[] stringToKyleByteArray(string input) {
            // A RELIABLE MANU-MANU STRING CONVERSION FROM TEXTBOX TO BYTE ARRAY
            char[] inputCharArray = input.ToCharArray();
            byte[] inputByteArray = Array.ConvertAll(inputCharArray, new Converter<char, byte>((char x) => { return (byte)x; }));
            return inputByteArray;
        }

    }
}
