using System;
using System.Drawing;
using System.Windows.Forms;

namespace XO {
    public partial class Form2 : Form {

        public Form HeadForm;

        Pen pen = new Pen(Color.Black, 5);

        Font font = new Font(FontFamily.GenericSerif, 30);

        SolidBrush sb = new SolidBrush(Color.Black);
        
        StringFormat sf = new StringFormat();

        bool win;

        int turn = 0,
            sizeX, sizeY,
            dx, dy; 

        int[] XO = new int[9],
              winCombination = new int[3];

        int[,] coordinateMap;

        public Form2() {
            InitializeComponent();
            sf.Alignment = StringAlignment.Center;

            pictureBox1.Refresh();
            resize();
            refreshData();
        }
        private void pictureBox1_Click(object sender, EventArgs e) {
            Point mouse = PointToClient(Cursor.Position);
            
            for (int position = 0; position < 9; position++) {
                if (mouse.X < coordinateMap[position, 0] && 
                    mouse.Y < coordinateMap[position, 1] + menuStrip1.Height && !win) {
                    if (XO[position] == -1) {
                        XO[position] = turn % 2;
                        checkCombination();
                        pictureBox1.Invalidate();
                        turn++;
                    }
                    break;
                }
            }
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e) {
            Refresh();
            refreshData();
            pictureBox1.Invalidate();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }
        private void Form2_Resize(object sender, EventArgs e) {
            resize();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e) {
            HeadForm.Show();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            for (int position = 0; position < 9; position++) {
                if (XO[position] == 1 || XO[position] == 0)
                    DrawImageXO(coordinateMap[position, 0] - dx, coordinateMap[position, 1] - dy, XO[position], e);
            }
            if (win) {
                String[] winner = { "Победили крестики!", "Победили нолики!", "Ничья!" };
                Point point = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2 - menuStrip1.Height);

                if (winCombination != null) {
                    e.Graphics.DrawLine(new Pen(turn % 2 == 0 ? Color.Blue : Color.Red, 5),
                                   coordinateMap[winCombination[0], 0] - dx, coordinateMap[winCombination[0], 1] - dy,
                                   coordinateMap[winCombination[1], 0] - dx, coordinateMap[winCombination[1], 1] - dy);
                    e.Graphics.DrawString(winner[turn % 2], font, sb, point, sf);
                }
                else
                    e.Graphics.DrawString(winner[2], font, sb, point, sf);
                
                
            }
        }

        void checkCombination() {
            pen.Color = turn%2 == 1 ? Color.Blue : Color.Red; 
            for (int i = 0; i < 3; i++) {
                if (XO[i * 3] == XO[i * 3 + 1] && XO[i * 3 + 1] == XO[i * 3 + 2] && XO[i * 3 + 2] == turn % 2) {
                    winCombination = new int[] { i * 3, i * 3 + 2, turn % 2 };
                    win = true;
                    return;
                }
                else if (XO[i] == XO[i + 3] && XO[i + 3] == XO[i + 6] && XO[i + 6] == turn % 2) {
                    winCombination = new int[] { i, i + 6, turn % 2 };
                    win = true;
                    return;
                }
            }
            if (XO[0] == XO[4] && XO[4] == XO[8] && XO[8] == turn % 2) {
                winCombination = new int[] { 0, 8, turn % 2 };
                win = true;
                return;
            }
            else if (XO[2] == XO[4] && XO[4] == XO[6] && XO[6] == turn % 2) {
                winCombination = new int[] { 2, 6, turn % 2 };
                win = true;
                return;
            }
            if (turn == 9 && !win) {
                winCombination = null;
                turn++;
                win = true;
            }
        }
        void DrawImageXO(int X, int Y, int turn, PaintEventArgs e) {
            Image TT = turn % 2 == 1 ? Properties.Resources.X : Properties.Resources._null;

            e.Graphics.DrawImage(TT, X - (sizeX / 2), Y - (sizeY / 2), sizeX, sizeY);
        }
        void refreshData() {
            for (int i = 0; i < 9; i++)
                XO[i] = -1;

            turn = 1;
            win = false;
            winCombination = null;
        }
        void resize() {
            int w = pictureBox1.Width, h = pictureBox1.Height;
            dx = w / 3 / 2;
            dy = h / 3 / 2;
            coordinateMap = new int[,] { { w / 3,     h / 3 },
                                         { w / 3 * 2, h / 3 }, 
                                         { w,         h / 3 },
                                         { w / 3,     h / 3 * 2 }, 
                                         { w / 3 * 2, h / 3 * 2 }, 
                                         { w,         h / 3 * 2 },
                                         { w / 3,     h }, 
                                         { w / 3 * 2, h}, 
                                         { w,         h } 
            };

            sizeX = pictureBox1.Width / 6;
            sizeY = pictureBox1.Height / 6;

            int width = Height - menuStrip1.Height*2;

            MaximumSize = new Size(width, 1000);
            MinimumSize = new Size(width, 300);

            font = new Font(FontFamily.GenericSerif, pictureBox1.Width / 15);

            pictureBox1.Refresh();
        }
    }
}
