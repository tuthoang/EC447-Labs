using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Slides : Form
    {

        private Bitmap bm;
        public int index = 0;
        private ListBox listPics = new ListBox();

        public Slides()
        {
            InitializeComponent();
        }
        //overload constructor to receive time and listbox
        public Slides(string time_interval, ListBox.ObjectCollection box)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.None;

            //set timer interval and turn it on
            timer1.Interval = int.Parse(time_interval)*1000;
            timer1.Enabled = true;

            //copy images from ListBox of Form1 into this list box
            listPics.Items.AddRange(box);

        }
        
        //press escape to close slideshow
        private void Slides_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        //After certain ticks, increment the index and update.
        private void timer1_Tick(object sender, EventArgs e)
        {
            index++;
            //close slide show when done iterating through list box
            if (index + 1 > listPics.Items.Count)
            {
                this.Close();
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //retrieve the current item in the listBox
            string currentImg = listPics.Items[index].ToString();

            
            try
            {
                //create bitmap of image
                bm = new Bitmap(currentImg);

                //images scaled to fit screen while maintaining aspect ratio
                SizeF cs = this.ClientSize;
                float ratio = Math.Min(cs.Height / bm.Height,
                cs.Width / bm.Width);

                //draw in center
                g.DrawImage(bm, (cs.Width - bm.Width * ratio) / 2f, (cs.Height - bm.Height * ratio) / 2f, bm.Width * ratio, bm.Height * ratio);
            }
            catch
            {
                Font myFont = new Font("Calibri", 28, FontStyle.Italic);                //if the file is not an image, display this message instead. 
                g.DrawString("Not an Image", myFont, Brushes.Red,20, 20);
            }
        }

    }
}
