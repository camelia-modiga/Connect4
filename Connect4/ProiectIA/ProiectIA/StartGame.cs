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
            button1.BackColor = jucator;
            button2.BackColor = calculator;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Connect4 form = new Connect4((int)numarRanduri.Value, (int)numarColoane.Value, jucator, calculator);
            form.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
