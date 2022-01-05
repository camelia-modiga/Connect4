using System;
using System.Drawing;
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
            graphics = panel.CreateGraphics();
        }

        private void ieșireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartGame startForm = new StartGame();
            startForm.ShowDialog();
            this.Close();
            
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (connect.mutareJucator(e.Location))
            {
                Invalidate(true);
                int castigator = connect.castigatorCurent();
                if (castigator == Connect.JUCATOR)
                {
                    MessageBox.Show("Ai câștigat!", "Joc terminat!");
                    this.Close();
                    return;
                }
                else if (castigator == Connect.REMIZA)
                {
                    MessageBox.Show("Remiză! Jucați din nou!", "Încearcă din nou!");
                    this.Close();
                    return;
                }

                connect.mutareCalculator();
                Invalidate(true);
                castigator = connect.castigatorCurent();

                if (castigator == Connect.CALCULATOR)
                {
                    MessageBox.Show("Ai pierdut!", "Joc terminat!");
                    this.Close();
                    return;
                }
                else if (castigator == Connect.REMIZA)
                {
                    MessageBox.Show("Remiză! Jucați din nou!", "Încearcă din nou!");
                    this.Close();
                    return;
                }
            }
            else
                MessageBox.Show("Faceți o altă mișcare!", "Mai incearca!");
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            connect.deseneazaTabla(graphics);
        }
    }
}
