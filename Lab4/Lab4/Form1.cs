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

namespace Lab4
{
    public partial class Form1 : Form
    {
        private int num_queens; //# of queens

        private int[,] array_of_q = new int[8, 8] ;  //2d array of an 8x8 board. This will contain the 0's or 1's (Q).

        public Form1()
        {
            InitializeComponent();
            this.Text = "Eight Queens By Tu Timmy Hoang";

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font newFont = new Font("Ariel", 30, FontStyle.Bold);  //Font for 'Q'
            //create 8x8 board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this.Hint_checkbox.Checked && !check(i, j)) //if the box is checked and the spots on the board are not valid, color them red
                    {
                        g.FillRectangle(Brushes.Red, 100 + i * 50, 100 + j * 50, 50, 50);
                        g.DrawRectangle(Pens.Black, 100 + i * 50, 100 + j * 50, 50, 50);
                    }
                    //draw white boxes
                    else if ((i + j) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.White, 100 + i * 50, 100 + j * 50, 50, 50);
                        g.DrawRectangle(Pens.Black, 100 + i * 50, 100 + j * 50, 50, 50);
                    }

                    //draw black boxes
                    else
                    {
                        g.FillRectangle(Brushes.Black, 100 + i * 50, 100 + j * 50, 50, 50);
                        g.DrawRectangle(Pens.Black, 100 + i * 50, 100 + j * 50, 50, 50);
                    }

                    //if the array contains a 1, draw onto to the board. Color is dependent on the background
                    if (array_of_q[i, j] == 1)
                    {
                        RectangleF rect = new RectangleF(100 + i * 50, 100 + j * 50, (float)50.0, (float)50.0);
                        if ((i + j) % 2 == 0|| this.Hint_checkbox.Checked && !check(i, j)) //Draw black 'Q' if the square is white.
                            g.DrawString("Q", newFont, Brushes.Black, rect);
                        else
                            g.DrawString("Q", newFont, Brushes.White, rect);
                    }
                }

            }
            //constantly draw to the screen the current # of queens
            g.DrawString("You have "+num_queens.ToString()+" queens on the board.", Font, Brushes.Black, 200, 50);
        }
        private bool check(int x, int y)
        {

            //rows
            for (int i = 0; i < 8; i++)
            {
                if (array_of_q[i, y] == 1)
                    return false;
            }
            //columns
            for (int i = 0; i < 8; i++)
            {
                if (array_of_q[x, i] == 1)
                    return false;
            }

            //diagonal up to left
            for (int j = y, x1=x;  x1 < 8 && j < 8; j++,x1++)
            {
                if (array_of_q[x1, j] == 1)
                {
                    return false;
                }

            }
            //diagonal up to right
            for (int j = y, x2 = x; x2 >= 0 && j < 8; j++, x2--)
            {
                if (array_of_q[x2, j] == 1)
                {
                    return false;
                }
            }

            //diagonal down to left
            for (int j = y, x3 = x; j >= 0 && x3 < 8; j--, x3++)
            {
                if (array_of_q[x3, j] == 1)
                {
                    return false;
                }
            }
            //diagonal down to right
            for (int j = y, x4 = x; x4 >= 0 && j >= 0; j--, x4--)
            {
                if (array_of_q[x4, j] == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if (((x >= 100) && (y >= 100)) && ((x < 500) && (y < 500)))//keep the clicks within the board, x and y are not not <= 500 because when subtracting by 100 and dividing by 50, that would give the box an index of 8 which is outside the range of array_of_q's.
            {
                x -= 100; //make 100,100 the origin
                y -= 100;

                //left click
                if (e.Button == MouseButtons.Left)
                {

                    //if the spot is valid, place a Queen down and increase num_queens
                    //diving by 50,gives us indices of the board 
                    if (check(x / 50, y / 50) == true)
                    {
                        array_of_q[x / 50, y / 50] = 1;
                        num_queens++;

                        this.Invalidate();
                        if (num_queens == 8)
                            MessageBox.Show("You did it!");
                    }
                    else
                    {
                        System.Media.SystemSounds.Beep.Play();

                    }
                }
                 //right click
                else if (e.Button == MouseButtons.Right)
                {
                    //if there is a queen in the spot already, remove it and decrement num_queens
                    if (array_of_q[x / 50, y / 50] == 1)
                    {
                        array_of_q [x / 50, y / 50] = 0;
                        num_queens--;
                        this.Invalidate();
                    }
                    else
                    {
                        System.Media.SystemSounds.Beep.Play();

                    }

                }
            }
        }

        //clear the board
        private void Clear_Click(object sender, EventArgs e)
        {
            Array.Clear(array_of_q,0,array_of_q.Length);
            num_queens = 0;
            this.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}