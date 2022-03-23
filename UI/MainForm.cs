using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HillCipher;

namespace Cryptography {
    public partial class Form1 : Form {
        private (string type, byte[] binary) cache = (null, null); // currently used to pass data (the algo type and binary data/byte array) between openFileDialog and saveFileDialog
        public Form1() {
            InitializeComponent();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e) {
            byte[] textboxByteArray = Commons.CustomStringFunctions.stringToDefaultByteArray(textBox.Text);
            byte[] encryptedByteArray = HillCipher.Core.Encrypt(textboxByteArray);

            // Convert the new byte[] into a char[] and then into a string.
            char[] encryptedChars = new char[Encoding.Default.GetCharCount(encryptedByteArray, 0, encryptedByteArray.Length)]; // initialize set char [] size
            Encoding.Default.GetChars(encryptedByteArray, 0, encryptedByteArray.Length, encryptedChars, 0); // actual byte[] to char[]
            string encryptedString = new string(encryptedChars); // char[] to string

            //// A RELIABLE MANU-MANU STRING CONVERSION FROM TEXTBOX TO BYTE ARRAY
            //byte[] textboxByteArray = Commons.CustomStringFunctions.stringToKyleByteArray(textBox.Text);
            //byte[] encryptedByteArray = HillCipher.Core.Encrypt(textboxByteArray);

            //// Convert the new byte[] into a char[] and then into a string.
            //char[] encryptedChars = Array.ConvertAll(encryptedByteArray, new Converter<byte, char>((byte x) => { return (char)x; }));
            //string encryptedString = new string(encryptedChars); // char[] to string

            textBox.Text = encryptedString;
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e) {
            byte[] textboxByteArray = Commons.CustomStringFunctions.stringToDefaultByteArray(textBox.Text);
            byte[] decryptedByteArray = HillCipher.Core.Decrypt(textboxByteArray);

            // Convert the new byte[] into a char[] and then into a string.
            char[] decryptedChars = new char[Encoding.Default.GetCharCount(decryptedByteArray, 0, decryptedByteArray.Length)]; // initialize set char [] size
            Encoding.Default.GetChars(decryptedByteArray, 0, decryptedByteArray.Length, decryptedChars, 0); // actual byte[] to char[]
            string decryptedString = new string(decryptedChars); // char[] to string

            //// A RELIABLE MANU-MANU STRING CONVERSION FROM TEXTBOX TO BYTE ARRAY
            //byte[] textboxByteArray = Commons.CustomStringFunctions.stringToKyleByteArray(textBox.Text);
            //byte[] decryptedByteArray = HillCipher.Core.Decrypt(textboxByteArray);

            //// Convert the new byte[] into a char[] and then into a string.
            //char[] decryptedChars = Array.ConvertAll(decryptedByteArray, new Converter<byte, char>((byte x) => { return (char)x; }));
            //string decryptedString = new string(decryptedChars); // char[] to string

            textBox.Text = decryptedString;
        }
    
    // HASHING CODE BLOCK -------------------------------------------------------------------------------------------------------------------------------------
        private void getMD5HashToolStripMenuItem_Click(object sender, EventArgs e) {
            string formatedTexBoxString = Commons.CustomStringFunctions.stringEnvironmentEndline(textBox.Text);
            byte[] textboxByteArray = Commons.CustomStringFunctions.stringToDefaultByteArray(formatedTexBoxString);

            byte[] hash = HashingFunction.MD5.ComputeHash(textboxByteArray);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                stringBuilder.Append(hash[i].ToString("x2")); // APPEND THE HEXADECIMAL VALUE OF THE CURRENT BYTE
            }

            MessageBox.Show(stringBuilder.ToString().ToUpper(), "MD5 Hash");
        }

        private void getSHA1HashToolStripMenuItem_Click(object sender, EventArgs e) {
            string formatedTexBoxString = Commons.CustomStringFunctions.stringEnvironmentEndline(textBox.Text);
            byte[] textboxByteArray = Commons.CustomStringFunctions.stringToDefaultByteArray(formatedTexBoxString);
            byte[] hash = HashingFunction.SHA.ComputeHash(textboxByteArray);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                stringBuilder.Append(hash[i].ToString("x2")); // APPEND THE HEXADECIMAL VALUE OF THE CURRENT BYTE
            }

            MessageBox.Show(stringBuilder.ToString().ToUpper(), "SHA1 Hash");
        }

    // CORE FILE MANIPULATION CODE BLOCK ----------------------------------------------------------------------------------------------------------------------
        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open Text File";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
        }

        private void encryptFileToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Encrypt";
            //openFileDialog1.Filter = "All files (*.*)|*.*|Hill Cipher Encrytion (*.hce)|*.*|Vigenere Encryption (*.vge)|*.*";
            openFileDialog1.Filter = "Hill Cipher Encrytion (*.hce)|*.*";
            //openFileDialog1.Filter = "All files (*.*)|*.*|Vigenere Encryption (*.vge)|*.*|Hill Cipher Encrytion (*.hce)|*.*";
            openFileDialog1.ShowDialog();
        }

        private void decryptFileToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Decrypt";
            openFileDialog1.Filter = "Hill Cipher Encrypted files (*.hce)|*.hce";
            openFileDialog1.ShowDialog();
        }

        private void hashFileToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Hash";
            //openFileDialog1.Filter = "All files (*.*)|*.*|Hill Cipher Encrytion (*.hce)|*.*|Vigenere Encryption (*.vge)|*.*";
            openFileDialog1.Filter = "MD5 Hashing|*.*|SHA1 Hashing|*.*";
            //openFileDialog1.Filter = "All files (*.*)|*.*|Vigenere Encryption (*.vge)|*.*|Hill Cipher Encrytion (*.hce)|*.*";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            byte[] fileByteArray;
            //char[] fileByteArrayToChars;
            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open)) {
                //fileByteArray = new byte[fs.Length];
                //fs.Read(fileByteArray, 0, (int)fs.Length);

                //fileByteArrayToChars = new char[Encoding.Default.GetCharCount(fileByteArray, 0, fileByteArray.Length)]; // initialize set char [] size
                //using (StreamReader sr = new StreamReader(fs, Encoding.Default)) {
                //    fileByteArray = new byte[fs.Length];
                //    sr.Read(fileByteArrayToChars, 0, (int)fs.Length);
                //}

                fileByteArray = new byte[fs.Length];
                fs.Read(fileByteArray, 0, (int)fs.Length);
            }

            //// Convert the new byte[] into a char[] and then into a string.
            //char[] fileByteArrayToChars = new char[Encoding.Default.GetCharCount(fileByteArray, 0, fileByteArray.Length)]; // initialize set char [] size
            //Encoding.Default.GetChars(fileByteArray, 0, fileByteArray.Length, fileByteArrayToChars, 0); // actual byte[] to char[]

            //string fileByteArrayString = new string(fileByteArrayToChars);
            //fileByteArray = Encoding.Default.GetBytes(fileByteArrayString);


            //string fileExtention = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
            //if (fileExtention == ".txt") {
            //    textBox.Text = Encoding.Default.GetString(fileByteArray);
            //}
            //else if (fileExtention == ".hce") {
            //    DialogResult userAction = MessageBox.Show("Selected a Hill Cipher Encrpted file, will not copy contents into textbox. Decryt?", "Decrypt?", MessageBoxButtons.YesNo);
            //    if (userAction == DialogResult.Yes) {
            //        //string we = Encoding.Default.GetString(fileByteArray);
            //        //char[] qw = Encoding.Default.GetString(fileByteArray).ToCharArray();
            //        //byte[] decryptedByteArray = HillCipher.Core.Decrypt(fileByteArray.Take(fileByteArray.Length - 1).ToArray());
            //        byte[] decryptedByteArray = HillCipher.Core.Decrypt(fileByteArray);
            //        //char[] asd = Encoding.Default.GetString(decryptedByteArray).ToCharArray();
            //        //char[] df = Encoding.UTF8.GetString(decryptedByteArray).ToCharArray();
            //        cache = ("hce", decryptedByteArray);

            //        string decryptedFileExtension = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 8, 4);
            //        string filePath = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length - 8);
            //        string actualFileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            //        saveFileDialog1.FileName = actualFileName; // Set filename
            //        saveFileDialog1.Filter = decryptedFileExtension.Substring(1).ToUpper() + " File|*" + decryptedFileExtension; // Set extention name
            //        saveFileDialog1.ShowDialog(); ;
            //    }
            //}
            //else {
            //    DialogResult userAction = MessageBox.Show("Encrypting files, will not copy contents into textbox", "Encrypt?", MessageBoxButtons.OKCancel);
            //    if (userAction == DialogResult.OK) {
            //        byte[] encryptedByteArray = HillCipher.Core.Encrypt(fileByteArray);
            //        cache = ("hce", encryptedByteArray);

            //        //string filePath = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length - 4);
            //        //string actualFileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            //        //saveFileDialog1.FileName = actualFileName + fileExtention + ".hce"; // Set filename
            //        string actualFileName = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1);
            //        saveFileDialog1.FileName = actualFileName +".hce"; // Set filename
            //        saveFileDialog1.Filter = "Hill Cipher Encrypted File|*.hce"; // Set extention name
            //        saveFileDialog1.ShowDialog(); ;
            //    }
            //}

            if (openFileDialog1.Title == "Open Text File") {
                textBox.Text = Encoding.Default.GetString(fileByteArray);
            }
            else if (openFileDialog1.Title == "Encrypt") {
                byte[] encryptedByteArray;
                switch (openFileDialog1.FilterIndex) { // not a zero based indexing, it start with 0
                    case 1: // Hill Climb
                        encryptedByteArray = HillCipher.Core.Encrypt(fileByteArray);
                        cache = ("hce", encryptedByteArray);
                        break;
                    default:
                        break;
                }

                string actualFileName = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1);
                saveFileDialog1.FileName = actualFileName + ".hce"; // Set filename/algorithm used
                saveFileDialog1.Filter = "Hill Cipher Encrypted File|*.hce"; // Set extention name
                saveFileDialog1.ShowDialog(); ;
            }
            else if (openFileDialog1.Title == "Decrypt") {
                string fileExtention = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4); // Determines what algorithm was used
                byte[] decryptedByteArray;
                if (fileExtention == ".hce") {
                    decryptedByteArray = HillCipher.Core.Decrypt(fileByteArray);
                    cache = ("hce", decryptedByteArray);
                }

                string decryptedFileExtension = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 8, 4);
                string filePath = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length - 8);
                string actualFileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                saveFileDialog1.FileName = actualFileName; // Set filename
                saveFileDialog1.Filter = decryptedFileExtension.Substring(1).ToUpper() + " File|*" + decryptedFileExtension; // Set extention name
                saveFileDialog1.ShowDialog(); ;
            }
            else {
                byte[] hash = HashingFunction.SHA.ComputeHash(fileByteArray);
                string messageBoxCaption = "";

                switch (openFileDialog1.FilterIndex) { // not a zero based indexing, it start with 0
                    case 1: // MD5
                        hash = HashingFunction.MD5.ComputeHash(fileByteArray);
                        messageBoxCaption = "MD5 Hash";
                        break;
                    case 2: // SHA1
                        hash = HashingFunction.SHA.ComputeHash(fileByteArray);
                        messageBoxCaption = "SHA1 Hash";
                        break;
                    default:
                        break;
                }

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++) {
                    stringBuilder.Append(hash[i].ToString("x2")); // APPEND THE HEXADECIMAL VALUE OF THE CURRENT BYTE
                }

                MessageBox.Show(stringBuilder.ToString().ToUpper(), messageBoxCaption);
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {
            if (cache.type == null) {
                //byte[] textboxByteArray = Encoding.UTF8.GetBytes(textBox.Text);
                //File.WriteAllText(saveFileDialog1.FileName, textBoxCharArray);

                char[] textBoxCharArray = textBox.Text.ToCharArray();
                byte[] textboxByteArray = Array.ConvertAll(textBoxCharArray, new Converter<char, byte>((char x) => { return (byte)x; }));
                using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create)) {
                    fs.Write(textboxByteArray, 0, textboxByteArray.Length);
                }
            }
            else {

                using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create)) {
                    fs.Write(cache.binary, 0, cache.binary.Length);
                }
                cache = (null, null); // empty cache
            }
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("" +
                "Hill cipher is a polygraphic substitution cipher based on linear algebra. To encrypt a message, an N component vector (considered as N bytes of a file) is multiplied by an invertible N×N matrix" +
                "This program sequentially encrypt N byte until every data of the message/file is processed. Whenever the last batch of bytes is less than N it appends bytes with a value of 0 to suffice the lacking bytes",
                "About Hill Cipher");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            //char[] textBoxCharArray = textBox.Text.ToCharArray();
            //string makes = "zin\r\n";

            //StringBuilder s = new StringBuilder();
            //s.Append(64);
            //s.Append("d");
            //s.Append("z");
            //s.AppendLine();
            //s.Append("\n");
            //s.Append("\r");
            //s.Append("\n");

            //StringBuilder tempString = new StringBuilder();
            //for (int i = 0; i < textBoxCharArray.Length; i++) {
            //    if (textBoxCharArray[i] == "\n".ToCharArray()[0]) {
            //        //tempString.Append("\r\n");
            //        tempString.AppendLine();
            //    }
            //    else {
            //        tempString.Append(textBoxCharArray[i]);
            //    }
            //}

            //s.AppendLine("jojo");
            ////textBox.Text = tempString.ToString();
            //textBox.Text = s.ToString();

            //textBox.SelectionStart = textBox.Text.Length;
            //textBox.SelectionLength = 0;
        }
    }
}
