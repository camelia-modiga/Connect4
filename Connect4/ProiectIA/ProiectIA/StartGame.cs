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
        Color jucator;
        Color calculator;

        public StartGame()
        {
            InitializeComponent();
            CenterToScreen();
            jucator = Color.Yellow;
            calculator = Color.Red;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGame form = new MainGame((int)numarRanduri.Value, (int)numarColoane.Value, jucator, calculator);
            form.ShowDialog();
            this.Close();
        }
    }
}
