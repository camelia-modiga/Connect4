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
    public partial class StartGame : Form
    {
        Color player1;
        Color player2;

        public StartGame()
        {
            InitializeComponent();
            CenterToScreen();
            player1 = Color.Yellow;
            player2 = Color.Red;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGame form = new MainGame((int)numarRanduri.Value, (int)numarColoane.Value, player1, player2);
            form.ShowDialog();
            this.Close();
        }
    }
}
