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
        private Graphics graphics;
        Connect connect;


        public MainGame(int L, int C, Color j, Color c)
        {
            InitializeComponent();
            CenterToScreen();
            connect = new Connect(L, C, panel.Height, panel.Width, j, c);
            DoubleBuffered = true;
            graphics = panel.CreateGraphics();
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
            if (connect.playerMove(e.Location))
            {
                Invalidate(true);
                int castigator = connect.checkWinner();
                if (castigator == Connect.PLAYER)
                {
                    MessageBox.Show("Ai câștigat!!", "Joc terminat!");
                    Close();
                    return;
                }
                //else if (castigator == Connect.COMPUTER)
                //{
                //    MessageBox.Show("Ai pierdut!!", "Joc terminat!");
                //    return;
                //}
                else if (castigator == Connect.DRAW)
                {
                    MessageBox.Show("Remiză! Jucați din nou!!", "Incearca din nou!");
                    Close();
                    return;
                }

                connect.computerMove();
                Invalidate(true);
                castigator = connect.checkWinner();
                //if (castigator == Connect.PLAYER)
                //{
                //    MessageBox.Show("Ai câștigat!", "Joc terminat!");
                //    Close();
                //    return;
                //}
                //else
                if (castigator == Connect.COMPUTER)
                {
                    MessageBox.Show("Ai pierdut!", "Joc terminat!");//, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }
                else if (castigator == Connect.DRAW)
                {
                    MessageBox.Show("Remiză! Jucați din nou!", "Incearca din nou!");
                    Close();
                    return;
                }
            }
            else
                MessageBox.Show("Faceți o altă mișcare!", "Mai incearca!");
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            connect.printBoard(graphics);
        }
    }
}
