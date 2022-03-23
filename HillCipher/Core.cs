using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipher {
    public class Core {

        //static private byte[,] key = {{ 6, 24, 1 },
        //               {13, 16, 10 },
        //               {20,17,15 }};

        //static private byte[,] keyInverse = {{ 8, 5, 10 },
        //                                     {21, 8, 21 },
        //                                     {21, 12, 8 }};

        static private byte[,] key = {{147, 69, 62 },
                                      {147, 232, 82},
                                      {29, 147, 147}};

        static private byte[,] keyInverse = {{ 82, 131, 218},
                                             {249, 171, 228},
                                             {57, 40, 241}};

        public static byte[] Encrypt(byte[] inputArrayByte) {
            //for (int i = 0; i < inputArrayByte.Length; i++) {
            //    if (inputArrayByte[i] > (byte)31) { // DONT TOUCH SPECIAL CHARACTER, RichTextBox CONVERTS CERTAIN SPECIAL CHARACTER TO ANOTHER. ex carriage return|\r|UTF-8(13) INTO newline|\n|UTF-8(10)
            //        inputArrayByte[i] += 3;
            //    }
            //}
            //return inputArrayByte;
            int lackingBytes = (3 - (inputArrayByte.Length % 3)) % 3; // THIS ASSURES THAT THE SIZE IS DIVISIBLE BY 3 AND WILL NOT ENCOUNTER IndexOutOfRangeException
            int[] encryptedArray = new int [inputArrayByte.Length + lackingBytes]; // DO NOT USE BYTE, IT IS LIMITED TO 255
            for (int byteIndex = 0; byteIndex < inputArrayByte.Length; byteIndex += 3) {
                int[] inputVector = new int[3];
                for (int i = 0; i < 3; i++) {
                    try {
                        //inputVector[i] = inputArrayByte[byteIndex + i] - 97;
                        inputVector[i] = inputArrayByte[byteIndex + i];
                    }
                    catch (IndexOutOfRangeException) {
                        inputVector[i] = 0;
                    }
                }

                //byte[] inputVector = { 0, 2, 19 };

                encryptedArray[byteIndex + 0] = key[0, 0] * inputVector[0] + key[0, 1] * inputVector[1] + key[0, 2] * inputVector[2];
                encryptedArray[byteIndex + 1] = key[1, 0] * inputVector[0] + key[1, 1] * inputVector[1] + key[1, 2] * inputVector[2];
                encryptedArray[byteIndex + 2] = key[2, 0] * inputVector[0] + key[2, 1] * inputVector[1] + key[2, 2] * inputVector[2];

                encryptedArray[byteIndex + 0] %= 256;
                encryptedArray[byteIndex + 1] %= 256;
                encryptedArray[byteIndex + 2] %= 256;

                //encryptedArray[byteIndex + 0] %= 26;
                //encryptedArray[byteIndex + 1] %= 26;
                //encryptedArray[byteIndex + 2] %= 26;

                //encryptedArray[byteIndex + 0] += 97;
                //encryptedArray[byteIndex + 1] += 97;
                //encryptedArray[byteIndex + 2] += 97;

                //encryptedByteArray
            }
            //return Array.ConvertAll(encryptedArray, new Converter<int, byte>((int x) => { return (byte)(x + 97); }));
            byte[] yow = Array.ConvertAll(encryptedArray, new Converter<int, byte>((int x) => { return (byte)x; }));
            return Array.ConvertAll(encryptedArray, new Converter<int, byte>((int x) => { return (byte)x; }));
        }
        public static byte[] Decrypt(byte[] inputArrayByte) {
            //for (int i = 0; i < inputArrayByte.Length; i++) {
            //    if (inputArrayByte[i] != (byte)10) { // DONT TOUCH SPECIAL CHARACTER, RichTextBox CONVERTS CERTAIN SPECIAL CHARACTER TO ANOTHER. ex carriage return|\r|UTF-8(13) INTO newline|\n|UTF-8(10)
            //        inputArrayByte[i] -= 3;
            //    }
            //}
            //return inputArrayByte;

            int lackingBytes = (3 - (inputArrayByte.Length % 3)) % 3; // THIS ASSURES THAT THE SIZE IS DIVISIBLE BY 3 AND WILL NOT ENCOUNTER IndexOutOfRangeException
            int[] decryptedArray = new int[inputArrayByte.Length + lackingBytes]; // DO NOT USE BYTE, IT IS LIMITED TO 255
            for (int byteIndex = 0; byteIndex < inputArrayByte.Length; byteIndex += 3) {
                int[] inputVector = new int[3];
                for (int i = 0; i < 3; i++) {
                    try {
                        //inputVector[i] = inputArrayByte[byteIndex + i] - 97;
                        inputVector[i] = inputArrayByte[byteIndex + i];
                    }
                    catch (IndexOutOfRangeException) {
                        inputVector[i] = 0;
                    }
                }
                decryptedArray[byteIndex + 0] = keyInverse[0, 0] * inputVector[0] + keyInverse[0, 1] * inputVector[1] + keyInverse[0, 2] * inputVector[2];
                decryptedArray[byteIndex + 1] = keyInverse[1, 0] * inputVector[0] + keyInverse[1, 1] * inputVector[1] + keyInverse[1, 2] * inputVector[2];
                decryptedArray[byteIndex + 2] = keyInverse[2, 0] * inputVector[0] + keyInverse[2, 1] * inputVector[1] + keyInverse[2, 2] * inputVector[2];

                decryptedArray[byteIndex + 0] %= 256;
                decryptedArray[byteIndex + 1] %= 256;
                decryptedArray[byteIndex + 2] %= 256;

                //decryptedArray[byteIndex + 0] %= 26;
                //decryptedArray[byteIndex + 1] %= 26;
                //decryptedArray[byteIndex + 2] %= 26;

                //decryptedArray[byteIndex + 0] += 97;
                //decryptedArray[byteIndex + 1] += 97;
                //decryptedArray[byteIndex + 2] += 97;

                //decryptedByteArray
            }
            //return Array.ConvertAll(decryptedArray, new Converter<int, byte>((int x) => { return (byte)(x + 97); }));
            return Array.ConvertAll(decryptedArray, new Converter<int, byte>((int x) => { return (byte)x; }));
        }
    }
}
