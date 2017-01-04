using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Lab7
{
    public partial class Form1 : Form
    {
        public byte[] key;
        public Form1()
        {
            InitializeComponent();
            this.Text = "File Encrypt/Decrypt by Tu Timmy Hoang";
        }

        private void source_Click(object sender, EventArgs e)
        {
            if (openDlg.ShowDialog(this) == DialogResult.OK)
                File_Name.Text = openDlg.FileName;
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            if (!errorDectectionEncrypt())
            {
                this.createKey();
                string ext = string.Concat(File_Name.Text, ".des");
                if (File.Exists(ext))
                {
                    if ((MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                        return;
                }

                EncryptData(File_Name.Text, ext, this.key, this.key);
            }

        }

        private void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            Console.WriteLine("Encrypting...");

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
       //         Console.WriteLine("{0} bytes processed", rdlen);
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            if (!errorDectectionDecrypt())
            {
                this.createKey();
                 string no_ext = File_Name.Text.Substring(0, File_Name.Text.Length - 3);
                if (File.Exists(no_ext))
                {
                    if ((MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                        return;
                }
                DecryptData(File_Name.Text, no_ext, key, key);
               
            }
        }
        private void DecryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            Console.WriteLine("Decrypting...");

            //Read from the input file, then decrypt and write to the output file.
            try
            {
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                    Console.WriteLine("{0} bytes processed", rdlen);
                }
                encStream.Close();
                fout.Close();
                fin.Close();
            }

            //if there is an error in matching keys, show message + delete newly created file
            catch
            {
                MessageBox.Show("Bad key or file","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                fout.Close();
                fin.Close();
                File.Delete(outName);

            }


        }

        private void createKey()
        {
            this.key = new byte[8];
            for(int i = 0; i < keytext.Text.Length; i++)
            {
                this.key[i%8] = (byte)(this.key[i%8] + (byte)keytext.Text[i]);
            }
        }

        private bool errorDectectionEncrypt()
        {
            //no key input
            if (this.keytext.Text == "")
            {

                MessageBox.Show("Please Enter a key.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return true;
            }
            if(this.File_Name.Text == "")
            {

                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (!File.Exists(File_Name.Text))
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }


        private bool errorDectectionDecrypt()
        {
            //no key input
            if (this.keytext.Text == "")
            {

                MessageBox.Show("Please Enter a key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            if (!File_Name.Text.EndsWith(".des"))
            {

                MessageBox.Show("Not a .des file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            if (this.File_Name.Text == "")
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (!File.Exists(File_Name.Text))
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
    }
}
