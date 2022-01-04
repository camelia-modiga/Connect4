using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIA
{
    class Connect
    {
        public readonly int L;
        public readonly int C;
        public int[,] tabla;
        private int circleWidth;
        private int circleHeight;
        private int panelWidth;
        private int panelHeight;
        public int Radius { get; set; }
        Color jucator;
        Color calculator;
        public Connect(int L, int C,int panelHeight, int panelWidth, Color j, Color c)
        {
            jucator = j;
            calculator = c;
            tabla = new int[L, C];
            this.L = L;
            this.C = C;
        
    
            this.panelHeight = panelHeight;
            this.panelWidth = panelWidth;
            circleWidth = panelWidth / C;
            circleHeight = panelHeight / L;
            Radius = Math.Min(circleWidth, circleHeight) / 2;
            Radius -= 5;
        }
        public void printBoard(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 2);
            graphics.Clear(Color.Blue);

            for (int i = 0; i < L; ++i)
            {
                for (int j = 0; j < C; ++j)
                {
                    Color color = Color.White;
                    if (tabla[i, j] == 1)
                        color = jucator;
                    if (tabla[i, j] == 2)
                        color = calculator;
                    Brush brush = new SolidBrush(color);
                    int x = (j * circleWidth) + (circleWidth / 2) - Radius;
                    int y = (i * circleHeight) + (circleHeight / 2) - Radius;
                    graphics.FillEllipse(brush, x, y, 2 * Radius, 2 * Radius);
                   
                }
            }
            
        }
    }
}
