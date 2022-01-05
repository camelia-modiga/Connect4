using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectIA
{
    public partial class MainGame : Form
    {
        private Graphics g;
        Connect game;

        public MainGame(int N, int M, Color p1, Color p2)
        {
            InitializeComponent();
            CenterToScreen();
            game = new Connect(N, M, panel.Height, panel.Width, p1, p2);
            DoubleBuffered = true;
            g = panel.CreateGraphics();
        }

        private void ieșireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
            this.Dispose();
        }

        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartGame startForm = new StartGame();
            startForm.ShowDialog();
            this.Close();
            this.Dispose();
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (game.playerMove(e.Location))
            {
                Invalidate(true);
                int winner = game.checkWinner();
                if (winner == Connect.PLAYER)
                {
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
                else if (winner == Connect.COMPUTER)
                {
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
                else if (winner == Connect.DRAW)
                {
                    MessageBox.Show("The board is full!", "Full!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }

                game.computerMove();
                Invalidate(true);
                winner = game.checkWinner();
                if (winner == Connect.PLAYER)
                {
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
                else if (winner == Connect.COMPUTER)
                {
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
                else if (winner == Connect.DRAW)
                {
                    MessageBox.Show("The board is full!", "Full!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
            }
            else
                MessageBox.Show("Invalid move! Make another choice!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            game.printBoard(g);
        }
    }
}
