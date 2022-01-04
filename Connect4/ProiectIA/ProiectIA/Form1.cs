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
    public partial class Connect4 : Form
    {
        private Graphics graphics;
        Connect connect;

        public Connect4(int L, int C, Color j, Color c)
        {
            InitializeComponent();
            CenterToScreen();
            connect = new Connect(L, C,panel.Height,panel.Width, j, c);
            DoubleBuffered = true;
            graphics = panel.CreateGraphics();

        }

        private void Connect4_Load(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            connect.printBoard(graphics);
        }
    }
}
