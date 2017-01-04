using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab5
{
    public partial class Form1 : Form
    {
        //store all shapes into my array list
        private ArrayList shapes = new ArrayList();

        //point of first/second clicks
        private Point init;
        private Point fin;

        //check if it is the first or second click
        private bool first_click = true;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Lab5 by Tu Timmy Hoang";
            //default
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
        }        


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //set the panel color to white
            panel1.BackColor = Color.White;

            //make the panel's height and width adjust to the size of the form
            panel1.Width = this.Width;
            panel1.Height = this.Height;

            Graphics g = e.Graphics;
            //draw each shapes onto the panel
            foreach (Shape s in shapes)
            {
                s.Draw(g);
            }
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //if it is currently the first click
            if (first_click)
            {
                //keep track of initial click
                init = e.Location;
                //set it to second click
                first_click = false;
                return;
            }

            //keep track of second click
            fin = e.Location;

            //reset to first click
            first_click = true;
            //default brush colors
            Brush pencolor= Brushes.White;
            Brush fillcolor=Brushes.White;
            Pen pen = new Pen(Brushes.White,1);

            //convert the pen width list box selection,which is a string, to a float.
            float pen_width = float.Parse((string)listBox3.SelectedItem);

            //set pencolor according the pen color list box selection
            if (listBox1.SelectedIndex == 0)
            {
                pencolor = Brushes.Black;
            }
            else if (listBox1.SelectedIndex == 1)
                pencolor = Brushes.Red;
            else if (listBox1.SelectedIndex == 2)
                pencolor = Brushes.Blue;
            else if (listBox1.SelectedIndex == 3)
                pencolor = Brushes.Green;
                
            //if outline or line is checked, create the pin according the pen_width and pencolor
            if (Line.Checked || Outline.Checked)
                pen = new Pen(pencolor, pen_width);
            //add it to shapes so it can be drawn
            if (Line.Checked)
                shapes.Add(new sLine(pen,init,fin));

            //if the fill box is checked off, set the fill color brush to the fillcolor list box selection
            if (Fill.Checked) {

                if (listBox2.SelectedIndex == 0)
                 {
                    fillcolor = Brushes.White;
                 }
                else if (listBox2.SelectedIndex == 1)
                    fillcolor = Brushes.Black;
                else if (listBox2.SelectedIndex == 2)
                    fillcolor = Brushes.Red;
                else if (listBox2.SelectedIndex == 3)
                    fillcolor = Brushes.Blue;
                else if (listBox2.SelectedIndex == 4)
                    fillcolor = Brushes.Green;
                if (!Outline.Checked)
                    pen = new Pen(fillcolor, pen_width);
            }

            //only add the rectangle/ellipse to the arraylist if either fill or outline is also checked off
            if ((Rectangle.Checked && Fill.Checked)|| (Rectangle.Checked && Outline.Checked))
                shapes.Add(new sRectangle(pen, init, fin, fillcolor));
            if ((Ellipse.Checked && Fill.Checked)|| (Ellipse.Checked && Outline.Checked))
                shapes.Add(new sEllipse(pen, init, fin, fillcolor));
            if (Text_button.Checked)
                shapes.Add(new sText(textBox1.Text, init, fin, pencolor, Font));

            //invalidate
            panel1.Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //press clear will clear the ararylist
            this.shapes.Clear();
            //invalidate
            this.panel1.Invalidate();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //only can undo something if there is atleast one object in the arraylist
            if (this.shapes.Count > 0)
                //remove the last object
                this.shapes.RemoveAt(this.shapes.Count - 1);
            //invalidate
            this.panel1.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close the form
            this.Close();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.LightGray;
            Graphics g = e.Graphics;
            Pen p = new Pen(Brushes.GhostWhite, 1);
            Rectangle d = new Rectangle(60, 14, 150, 166);
            g.DrawRectangle(p, d);
        }

    }

    //base class with virtual draw method
    public class Shape
    {
        public virtual void Draw(Graphics g) { }
    }

    //line class 
    public class sLine : Shape
    {
        //takes 2 points and pen
        private Point initial;
        private Point final;
        private Pen pen;
       
        public sLine(Pen pen, Point initial, Point final)
        {
            this.pen = pen;
            this.initial = initial;
            this.final = final;
        }
        //draw the line
        public override void Draw(Graphics g)
        {
            g.DrawLine(this.pen, this.initial, this.final);
        }
    }

    //rectangle class
    public class sRectangle : Shape
    {
        //takes 2 points, a pen and brush
        private Point initial;
        private Point final;
        private Pen pen;
        private Brush b;
        public sRectangle(Pen pen, Point initial, Point final, Brush b)
        {
            this.pen = pen;
            this.b = b;
            this.initial = initial;
            this.final = final;
        }

        public override void Draw(Graphics g)
        {
            //x min is the smallest x of the two points so we can draw from left to right
            int x_min = Math.Min(initial.X, final.X);
            //y min is the smallest y of the two points so we can draw from top to bottom
            int y_min = Math.Min(initial.Y, final.Y);
            
            //width is the distance between the x's
            int width = initial.X - final.X;

            //height is the distance between the y's
            int height = initial.Y - final.Y;

            //have to take abs values in case of negative values
            g.FillRectangle(b, x_min, y_min, Math.Abs(width), Math.Abs(height));
            g.DrawRectangle(pen, x_min, y_min, Math.Abs(width), Math.Abs(height));
        }
    }

    public class sEllipse : Shape
    {
        private Point initial;
        private Point final;
        private Pen pen;
        private Brush b;
        public sEllipse(Pen pen, Point initial, Point final, Brush b)
        { 
            this.pen = pen;
            this.initial = initial;
            this.final = final;
            this.b = b;
        }

        public override void Draw(Graphics g)
        {
            int x_min = Math.Min(initial.X, final.X);
            int y_min = Math.Min(initial.Y, final.Y);
            int width = initial.X - final.X;
            int height = initial.Y - final.Y;
            g.FillEllipse(b, x_min, y_min, Math.Abs(width), Math.Abs(height));
            g.DrawEllipse(pen, x_min, y_min, Math.Abs(width), Math.Abs(height));
        }
    }

    public class sText : Shape
    {
        private Point initial;
        private Point final;
        private Brush b;
        private string text;
        private Font font;
        public sText(string text, Point initial, Point final, Brush b,Font font)
        {
            this.text = text;
            this.initial = initial;
            this.final = final;
            this.b = b;
            this.font = font; 
        }

        public override void Draw(Graphics g)
        {
            int x_min = Math.Min(initial.X, final.X);
            int y_min = Math.Min(initial.Y, final.Y);
            int width = initial.X - final.X;
            int height = initial.Y - final.Y;
            Rectangle R = new Rectangle(x_min, y_min, Math.Abs(width), Math.Abs(height));
            g.DrawString(text, font, b, R);
        }
    }
}


