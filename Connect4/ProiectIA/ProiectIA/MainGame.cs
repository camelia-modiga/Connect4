﻿using System;
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
        /// <summary>
        /// Metoda pentru inchiderea aplicatiei
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ieșireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Metoda pentru inceperea unui joc nou, indiferent de momentul in care se afla jocul curent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartGame startForm = new StartGame();
            startForm.ShowDialog();
            this.Close();
            
        }
        /// <summary>
        /// Metoda pentru a decide daca mutarea se poate realiza si daca influenteaza terminarea sau continuarea jocului
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            /// daca mutarea jucatorului este decisiva, se afiseaza un mesaj corespunzator 
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

                /// daca mutarea calculatorului este decisiva, se afiseaza un mesaj corespunzator 
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
            ///daca mutarea nu este permisa, se afiseaza un mesaj corespunzator
            else
                MessageBox.Show("Faceți o altă mișcare!", "Mai incearca!");
        }

        /// <summary>
        /// Metoda pentru desenarea tablei de joc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            connect.deseneazaTabla(graphics);
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" 1.Tu începi jocul!\n 2.Calculatorul pune o piesă imediat după tine! \n 3.Câștigă cine pune primul 4 bile în ordine (diagonală/verticală/orizontală) \n 4.Ai pierdut?Joacă din nou! :)", "Reguli!");
            return;
        }
    }
}
