using System;
using System.Drawing;
using System.Windows.Forms;


namespace Lab6
{
    public class GameEngine
    {
        private const float clientSize = 100;
        private const float lineLength = 80;
        private const float block = lineLength / 3;

        public enum CellSelection { N, O, X };
        public CellSelection[,] grid = new CellSelection[3, 3];

        public bool pcWin;
        public bool userWin;
        public int turns;
        public bool gameDone;
        public bool Draw;
        public bool firstPCTurn;
        public bool pcTurn;

        //all ways of winning for O and X
        public int row0_o=0;
        public int row1_o=0;
        public int row2_o=0;
        public int col0_o=0;
        public int col1_o=0;
        public int col2_o=0;
        public int diag0_o=0;
        public int diag1_o=0;

        public int row0_x=0;
        public int row1_x=0;
        public int row2_x=0;
        public int col0_x=0;
        public int col1_x=0;
        public int col2_x=0;
        public int diag0_x=0;
        public int diag1_x=0;

        public GameEngine()
        {
            gameDone = false;
            firstPCTurn = false;
            turns = 0;
        }
        public GameEngine userClick(MouseEventArgs e, PointF[] p, GameEngine board)
        {
            if (turns != 0)
            {
                firstPCTurn = false;
            }
            if (p[0].X < 0 || p[0].Y < 0) return board;
            int i = (int)(p[0].X / block);
            int j = (int)(p[0].Y / block);
            if (i > 2 || j > 2) return board;
            if (grid[i, j] == GameEngine.CellSelection.N && !gameDone&& !board.pcTurn)
            {
                if (e.Button == MouseButtons.Left)
                {
                    grid[i, j] = GameEngine.CellSelection.X;
                    board.pcTurn = true;

                }
                board.turns++;
                if (winner(board))
                {
                    gameDone = true;
                    return board;
                }

                if (!gameDone)
                {
                    if (board.pcTurn)
                        this.pcMove(board);
                    return board;
                }                           
            }
            
            else if ((grid[i,j]==GameEngine.CellSelection.O || grid[i,j]==GameEngine.CellSelection.X) && !gameDone)
            {
                MessageBox.Show("Invalid move");
                return board;
            }
            else
            {
                MessageBox.Show("Game is over. No further moves allowed. Start new game to continue.");
                return board;
            }
            return board;
        }

        public void pcMove(GameEngine board)
        {
            bool currentmove = true;
            //if the pc goes first, choose a random location to begin with
            if (firstPCTurn)
            {
                Random rand = new Random();
                int randomNum = rand.Next(0, 3);
                board.grid[randomNum, randomNum] = GameEngine.CellSelection.O;
                firstPCTurn = false;
            }
            else
            {
                //rows
                for (int j = 0; j < 3; ++j)
                {
                    //columns
                    for (int i = 0; i < 3; ++i)
                    {
                        //check for O's
                        if (board.grid[i, j] == GameEngine.CellSelection.O)
                        {
                            if (i == 0 && j == 0)
                            {
                                row0_o++;
                                col0_o++;
                                diag0_o++;
                            }
                            if (i == 1 && j == 0)
                            {
                                row0_o++;
                                col1_o++;
                            }
                            if (i == 2 && j == 0)
                            {
                                row0_o++;
                                col2_o++;
                                diag1_o++;
                            }
                            if (i == 0 && j == 1)
                            {
                                row1_o++;
                                col0_o++;
                            }
                            if (i == 1 && j == 1)
                            {
                                row1_o++;
                                col1_o++;
                                diag0_o++;
                                diag1_o++;
                            }
                            if (i == 2 && j == 1)
                            {
                                row1_o++;
                                col2_o++;
                            }
                            if (i == 0 && j == 2)
                            {
                                row2_o++;
                                col0_o++;
                                diag1_o++;
                            }
                            if (i == 1 && j == 2)
                            {
                                row2_o++;
                                col1_o++;
                            }
                            if (i == 2 && j == 2)
                            {
                                row2_o++;
                                col2_o++;
                                diag0_o++;
                            }
                        }
                        //check for X's
                        if (board.grid[i, j] == GameEngine.CellSelection.X)
                        {
                            if (i == 0 && j == 0)
                            {
                                row0_x++;
                                col0_x++;
                                diag0_x++;
                            }
                            if (i == 1 && j == 0)
                            {
                                row0_x++;
                                col1_x++;
                            }
                            if (i == 2 && j == 0)
                            {
                                row0_x++;
                                col2_x++;
                                diag1_x++;
                            }
                            if (i == 0 && j == 1)
                            {
                                row1_x++;
                                col0_x++;
                            }
                            if (i == 1 && j == 1)
                            {
                                row1_x++;
                                col1_x++;
                                diag0_x++;
                                diag1_x++;
                            }
                            if (i == 2 && j == 1)
                            {
                                row1_x++;
                                col2_x++;
                            }
                            if (i == 0 && j == 2)
                            {
                                row2_x++;
                                col0_x++;
                                diag1_x++;
                            }
                            if (i == 1 && j == 2)
                            {
                                row2_x++;
                                col1_x++;
                            }
                            if (i == 2 && j == 2)
                            {
                                row2_x++;
                                col2_x++;
                                diag0_x++;
                            }
                        }
                    }
                }
                //pc moves

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                       
                        if ((diag0_o == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag0_o == 2 && board.grid[1, 1] == GameEngine.CellSelection.N) )
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag0_o == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //diags 1
                        if ((diag1_o == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag1_o == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag1_o == 2 && board.grid[2, 0] == GameEngine.CellSelection.N) )
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_o == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_o == 2 && board.grid[1, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_o == 2 && board.grid[2, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //row1
                        if ((row1_o == 2 && board.grid[0, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row1_o == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row1_o == 2 && board.grid[2, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //row2
                        if ((row2_o == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row2_o == 2 && board.grid[1, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row2_o == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }

                        //col 0
                        if ((col0_o == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col0_o == 2 && board.grid[0, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col0_o == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //col1
                        if ((col1_o == 2 && board.grid[1, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col1_o == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col1_o == 2 && board.grid[1, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //col2
                        if ((col2_o == 2 && board.grid[2, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col2_o == 2 && board.grid[2, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col2_o == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //defense
                        if ((diag0_x == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag0_x == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag0_x == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //diags 1
                        if ((diag1_x == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag1_x == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((diag1_x == 2 && board.grid[2, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_x == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_x == 2 && board.grid[1, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row0_x == 2 && board.grid[2, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //row1
                        if ((row1_x == 2 && board.grid[0, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row1_x == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row1_x == 2 && board.grid[2, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //row2
                        if ((row2_x == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row2_x == 2 && board.grid[1, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((row2_x == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }

                        //col 0
                        if ((col0_x == 2 && board.grid[0, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col0_x == 2 && board.grid[0, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col0_x == 2 && board.grid[0, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //col1
                        if ((col1_x == 2 && board.grid[1, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col1_x == 2 && board.grid[1, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col1_x == 2 && board.grid[1, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[1, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //col2
                        if ((col2_x == 2 && board.grid[2, 0] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col2_x == 2 && board.grid[2, 1] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if ((col2_x == 2 && board.grid[2, 2] == GameEngine.CellSelection.N))
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        //moves that occur before a winning move

                        if (board.grid[1, 1] == GameEngine.CellSelection.N)
                        {
                            board.grid[1, 1] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if (board.grid[0, 0] == GameEngine.CellSelection.N)
                        {
                            board.grid[0, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if (board.grid[0, 2] == GameEngine.CellSelection.N)
                        {
                            board.grid[0, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if (board.grid[2, 0] == GameEngine.CellSelection.N)
                        {
                            board.grid[2, 0] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                        if (board.grid[2, 2] == GameEngine.CellSelection.N)
                        {
                            board.grid[2, 2] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }

                        // default move
                        if (board.grid[j, i] == GameEngine.CellSelection.N)
                        {
                            board.grid[i, j] = GameEngine.CellSelection.O;
                            currentmove = false;
                            break;
                        }
                    }
                    if (!currentmove)
                        break;
                }
            }
            if (this.winner(board))
            {
                gameDone = true;
                return;
            }

        //reset the variables so they can be counted correctly
        row0_o = 0;
        row1_o = 0;
        row2_o = 0;
        col0_o = 0;
        col1_o = 0;
        col2_o = 0;
        diag0_o = 0;
        diag1_o = 0;
        row0_x = 0;
        row1_x = 0;
        row2_x = 0;
        col0_x = 0;
        col1_x = 0;
        col2_x = 0;
        diag0_x = 0;
        diag1_x = 0;
        board.pcTurn = false;
        }
        public bool winner(GameEngine board)
        {
            //check columns for computer wins
            if (board.grid[0,0]==GameEngine.CellSelection.O && board.grid[0, 1] == GameEngine.CellSelection.O && board.grid[0, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            if (board.grid[1, 0] == GameEngine.CellSelection.O && board.grid[1, 1] == GameEngine.CellSelection.O && board.grid[1, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            if (board.grid[2, 0] == GameEngine.CellSelection.O && board.grid[2, 1] == GameEngine.CellSelection.O && board.grid[2, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            //check rows for computer wins
            if (board.grid[0, 0] == GameEngine.CellSelection.O && board.grid[1, 0] == GameEngine.CellSelection.O && board.grid[2, 0] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            if (board.grid[0, 1] == GameEngine.CellSelection.O && board.grid[1, 1] == GameEngine.CellSelection.O && board.grid[2, 1] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            if (board.grid[0, 2] == GameEngine.CellSelection.O && board.grid[1, 2] == GameEngine.CellSelection.O && board.grid[2, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            //check diags for computer wins
            if (board.grid[0, 0] == GameEngine.CellSelection.O && board.grid[1, 1] == GameEngine.CellSelection.O && board.grid[2, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }
            if (board.grid[2, 0] == GameEngine.CellSelection.O && board.grid[1,1] == GameEngine.CellSelection.O && board.grid[0, 2] == GameEngine.CellSelection.O)
            {
                pcWin = true;
                MessageBox.Show("Computer wins");
                return pcWin;
            }

            //check rows for user wins
            if (board.grid[0, 0] == GameEngine.CellSelection.X && board.grid[0, 1] == GameEngine.CellSelection.X && board.grid[0, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (board.grid[1, 0] == GameEngine.CellSelection.X && board.grid[1, 1] == GameEngine.CellSelection.X && board.grid[1, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (board.grid[2, 0] == GameEngine.CellSelection.X && board.grid[2, 1] == GameEngine.CellSelection.X && board.grid[2, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            //check rows for computer wins
            if (board.grid[0, 0] == GameEngine.CellSelection.X && board.grid[1, 0] == GameEngine.CellSelection.X && board.grid[2, 0] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (board.grid[0, 1] == GameEngine.CellSelection.X && board.grid[1, 1] == GameEngine.CellSelection.X && board.grid[2, 1] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (board.grid[0, 2] == GameEngine.CellSelection.X && board.grid[1, 2] == GameEngine.CellSelection.X && board.grid[2, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            //check diags for computer wins
            if (board.grid[0, 0] == GameEngine.CellSelection.X && board.grid[1, 1] == GameEngine.CellSelection.X && board.grid[2, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (board.grid[2, 0] == GameEngine.CellSelection.X && board.grid[1, 1] == GameEngine.CellSelection.X && board.grid[0, 2] == GameEngine.CellSelection.X)
            {
                userWin = true;
                MessageBox.Show("User wins");
                return userWin;
            }
            if (turns >= 5)
            {
                Draw = true;
                MessageBox.Show("DRAW!");
                return Draw;
            }
            //default
            return false;
        }

    }
}