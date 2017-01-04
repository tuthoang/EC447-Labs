using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab6
{
    public partial class Form1 : Form
    {
        //dimensions 
        private const float clientSize = 100;
        private const float lineLength = 80;
        private const float block = lineLength / 3;
        private const float offset = 10;
        private const float delta = 5;
        private float scale; //current scale factor 
        public GameEngine board;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            this.Text = "TicTacToe by Tu Timmy Hoang";
            board = new GameEngine();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ApplyTransform(g);
            //draw board
            g.DrawLine(Pens.Black, block, 0, block, lineLength);
            g.DrawLine(Pens.Black, 2 * block, 0, 2 * block, lineLength);
            g.DrawLine(Pens.Black, 0, block, lineLength, block);
            g.DrawLine(Pens.Black, 0, 2 * block, lineLength, 2 * block);
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    if (board.grid[i, j] == GameEngine.CellSelection.O) DrawO(i, j, g);
                    else if (board.grid[i, j] == GameEngine.CellSelection.X) DrawX(i, j,
                    g);
        }
        private void ApplyTransform(Graphics g)
        {
            scale = Math.Min(ClientRectangle.Width / clientSize,
            ClientRectangle.Height / clientSize);
            if (scale == 0f) return;
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(offset, offset);
        }
        private void DrawX(int i, int j, Graphics g)
        {
            g.DrawLine(Pens.Black, i * block + delta, j * block + delta,
            (i * block) + block - delta, (j * block) + block - delta);
            g.DrawLine(Pens.Black, (i * block) + block - delta,
            j * block + delta, (i * block) + delta, (j * block) + block - delta);
        }
        private void DrawO(int i, int j, Graphics g)
        {
            g.DrawEllipse(Pens.Black, i * block + delta, j * block + delta,
            block - 2 * delta, block - 2 * delta);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            ApplyTransform(g);
            PointF[] p = { new Point(e.X, e.Y) };
            g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, p);
            board = board.userClick(e, p, board);
            if (board.turns != 0)
                computerStartsToolStripMenuItem.Enabled = false;
            Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            board = new GameEngine();
            computerStartsToolStripMenuItem.Enabled = true;

            Invalidate();
        }

        private void computerStartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            board.firstPCTurn = true;
            board.pcMove(board);
            computerStartsToolStripMenuItem.Enabled = false;
            Invalidate();
        }
    }
}
