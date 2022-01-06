using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProiectIA
{
    /// <summary>
    /// Clasa care se ocupa de componenta aplicatiei care permite pornirea jocului
    /// </summary>
    public partial class StartGame : Form
    {
        public StartGame()
        {
            InitializeComponent();
            CenterToScreen();
        }

        /// <summary>
        /// Metoda care permite inceperea unui nou joc
        /// De pe interfata se alege numarul de linii si de coloane corespunzatoare tablei de joc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGame form = new MainGame((int)numarRanduri.Value, (int)numarColoane.Value);
            form.ShowDialog();
            this.Close();
        }
    }
}
