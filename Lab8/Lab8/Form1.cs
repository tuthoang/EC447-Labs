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

namespace Lab8
{
    public partial class Form1 : Form
    {
        private Form f = new Form();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Slide Show Viewer by Tu Timmy Hoang";

            //allow for multiple items to be selected at once
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            //default time interval
            interval_textBox.Text = "5";

        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //each item selected in the file dialog gets added to the list box
                foreach(string s in openFileDialog1.FileNames)
                 listBox1.Items.Add(s);
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            //remove multiple files at once from listbox
            for (int x = listBox1.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = listBox1.SelectedIndices[x];
                listBox1.Items.RemoveAt(idx);
            }
        }
        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "*.jpg;*.gif;*.png,*.bmp|*.jpg;*.gif;*.png;*.bmp|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
        }

        private void Show_button_Click(object sender, EventArgs e)
        {
            //No items in listbox
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No images to Show.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            try
            {
                //call the slide form
                Slides slide = new Slides(interval_textBox.Text, listBox1.Items);
                slide.ShowDialog();
            }

            //invalid time interval
            catch
            {
                MessageBox.Show("Please enter an integer time interval > 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //filter
            this.saveFileDialog1.Filter = "*.pix| *.pix";

            //Don't save empty list box 
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No file names to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //open the stream writer
                StreamWriter SaveFile = new StreamWriter(saveFileDialog1.OpenFile());

                //save each item in list box to stream writer
                foreach (object item in listBox1.Items)
                {
                    SaveFile.WriteLine(item.ToString());
                }
                SaveFile.Close();
            }
        }

        private void openCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //filter
            this.openFileDialog1.Filter = " *.pix| *.pix";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //clear all items in list box
                listBox1.Items.Clear();

                //open the stream reader
                StreamReader ReadFile = new StreamReader(openFileDialog1.OpenFile());
                string line;

                while ((line = ReadFile.ReadLine()) != null)
                {
                    //string line = ReadFile.ReadLine();
                    //add items into list box
                    listBox1.Items.Add(line);
                }
                ReadFile.Close();
            }
        }

    }
}
