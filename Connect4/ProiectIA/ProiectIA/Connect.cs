﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectIA
{
    class Move
    {
        public int mutare;
        public int scor;

        public Move(int m, int s)
        {
            mutare = m;
            scor = s;
        }
    }

    class Connect
    {
        public int L;
        public int C;

        public const int JUCATOR = 1;
        public const int CALCULATOR = 2;
        public const int REMIZA = 3;
        public int[,] tabla;

        public List<string> mutariCalculator = new List<string>(new string[] { "0002", "0020", "0022", "0200", "0202", "0220", "0222", "2000", "2002", "2020", "2022", "2200", "2202", "2220", "2222" });
        public List<string> mutariJucator = new List<string>(new string[] { "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" });
      
        private int latime;
        private int inaltime;

        public int raza { get; set; }

        Color jucator;
        Color calculator;

        public Connect(int L, int C, int inaltimePanou, int latimePanou, Color j, Color c)
        {
            jucator = j;
            calculator = c;
            tabla = new int[L, C];
            this.L= L;
            this.C = C;

            this.inaltime = inaltimePanou ;
            this.latime = latimePanou;
            raza = Math.Min(latime/ C, inaltime / L) / 2 - 5; 
        }
        /// <summary>
        /// Metoda folosita pentru desenarea tablei de joc
        /// </summary>
        /// <param name="graphics"></param>
        public void deseneazaTabla(Graphics graphics)
        {
            graphics.Clear(Color.Blue);
            for (int i = 0; i < L; ++i)
            {
                for (int j = 0; j < C; ++j)
                {
                    Color color = Color.Black;
                    if (tabla[i, j] == 1)
                        color = jucator;
                    if (tabla[i, j] == 2)
                        color = calculator;
                    Brush brush = new SolidBrush(color);
                    int bilaX = (j * (latime / C)) + ((latime / C) / 2) - raza;
                    int bilaY = (i * (inaltime / L)) + ((inaltime / L) / 2) - raza;
                    graphics.FillEllipse(brush, bilaX, bilaY, 2 * raza, 2 * raza);
                    brush.Dispose();
                }
            }
        }

        /// <summary>
        /// Metoda care verifica daca exista un castigator
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public int verificaCastigator(int[,] tabla)
        {
            for (int i = 0; i < L; i++)
                for (int j = 0; j <= C - 4; j++)
                    if (tabla[i, j] != 0 && tabla[i, j + 1] == tabla[i, j] && tabla[i, j + 2] == tabla[i, j] && tabla[i, j + 3] == tabla[i, j])
                        return tabla[i, j];


            for (int i = 0; i <= L - 4; i++)
                for (int j = 0; j <= C - 4; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j + 1] == tabla[i, j] && tabla[i + 2, j + 2] == tabla[i, j] && tabla[i + 3, j + 3] == tabla[i, j])
                        return tabla[i, j];

            for (int i = 0; i <= L - 4; i++)
                for (int j = 0; j < C; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j] == tabla[i, j] && tabla[i + 2, j] == tabla[i, j] && tabla[i + 3, j] == tabla[i, j])
                        return tabla[i, j];

            for (int i = 0; i <= L - 4; i++)
                for (int j = 3; j < C; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j - 1] == tabla[i, j] && tabla[i + 2, j - 2] == tabla[i, j] && tabla[i + 3, j - 3] == tabla[i, j])
                        return tabla[i, j];
            return -1;

        }

        /// <summary>
        /// Metoda care verifica daca in starea actuala exista un castigator si il returneaza daca exista 
        /// </summary>
        /// <returns></returns>
        public int castigatorCurent()
        {
            int aux = 1;
            for (int i = 0; i < L; ++i)
                for (int j = 0; j < C; ++j)
                    if (tabla[i, j] == 0)
                        aux = 0;

            if (aux == 1) 
                return REMIZA;

            return verificaCastigator(tabla);
        }
        /// <summary>
        /// Metoda care reprezinta coloana pe care jucatorul a ales-o, mutarea facuta de acesta
        /// </summary>
        /// <param name="locatie"></param>
        /// <returns></returns>
        public bool mutareJucator(Point locatie)
        {
            int alegere = locatie.X / (latime / C);
            if (!mutareValida(tabla).Contains(alegere))
                return false;
            for (int i = L - 1; i >= 0; i--)
            {
                if (tabla[i, alegere] == 0)
                {
                    tabla[i, alegere] = 1;
                    break;
                }
            }
            return true;
        }

        public void mutareCalculator()
        {   
            ///algoritmul Minimax se apeleaza dupa fiecare mutare a jucatorului
            Move m = algoritmMinimax(tabla, 0, 2, int.MinValue, int.MaxValue);

            if (m.mutare != -1 && m.mutare != -2)
            {
                for (int i = L - 1; i >= 0; --i)
                {
                    /// daca exista spatiu pe tabla, atunci se pune piesa (pe primul loc liber)
                    if (tabla[i, m.mutare] == 0)
                    {
                        tabla[i, m.mutare] = 2;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// returneaza coloanele pe care mai sunt acceptate mutari
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        private List<int> mutareValida(int[,] tabla)
        {
            List<int> mutariValide = new List<int>();
            for (int i = 0; i < C; i++)
                if (tabla[0, i] == 0) 
                    mutariValide.Add(i);
            return mutariValide;
        }

        /// <summary>
        /// Metoda ce primeste starea curenta, ce jucator a facut mutare si in ce coloana 
        /// Returneaza primul spatiu liber din acea coloana
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="coloana"></param>
        /// <param name="jucator"></param>
        /// <returns></returns>
        private int[,] mutareNoua(int[,] tabla, int coloana, int jucator)
        {
            int[,] aux = new int[L,C];
            for (int i = 0; i < L; i++)
                for (int j = 0; j < C; j++)
                    aux[i, j] = tabla[i, j];

            for (int i = L - 1; i >= 0; i--)
            {
                if (aux[i, coloana] == 0)
                {
                    aux[i, coloana] = jucator;
                    break;
                }
            }
            return aux;
        }
        public int numarare(string s, int nr)
        {
            if (mutariCalculator.Contains(s))
                nr++;
            else if (mutariJucator.Contains(s))
                nr--;
            return nr;
        }

        /// <summary>
        /// Functia de evaluare
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public int functieEvaluare(int[,] tabla)
        {
            int castigator = verificaCastigator(tabla);
            string s;
            if (castigator != -1)
            {
                if (castigator == 1)
                    return -1 * int.MaxValue;
                else
                    return int.MaxValue;
            }
            int nr = 0;
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j <= C - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i, j + 1], tabla[i, j + 2], tabla[i, j + 3]);
                    nr = numarare(s, nr);
                }
            }
           
            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 0; j <= C - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j + 1], tabla[i + 2, j + 2], tabla[i + 3, j + 3]);
                    nr = numarare(s, nr);
                }
            }

            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j], tabla[i + 2, j], tabla[i + 3, j]);
                    nr = numarare(s, nr);
                }
            }
            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 3; j < C; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j - 1], tabla[i + 2, j - 2], tabla[i + 3, j - 3]);
                    nr = numarare(s, nr);
                }
            }
            return nr;
        }

        /// <summary>
        /// Algoritumul minimax cu retezare alfa beta
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="depth"></param>
        /// <param name="jucator"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Move algoritmMinimax(int[,] tabla, int depth, int jucator, int a, int b)
        {
            int alfa = a;
            int beta = b;
            int castigator = verificaCastigator(tabla);

            ///adancimear arborelui este 3
            if (depth == 3)
                return new Move(-1, functieEvaluare(tabla));
            else if (castigator != -1)
                if (castigator == 1)
                    return new Move(-1, -1 * int.MaxValue);
                else
                    return new Move(-1, int.MaxValue);
            else
            {
                List<int> mutari = mutareValida(tabla);
                if (mutari.Count == 0)
                    return new Move(-2, functieEvaluare(tabla));
                else
                {
                    ///daca nodul este maxim
                    if (jucator != 1)
                    {
                        int scorulPerfect = int.MinValue, mutareaPerfecta = -1000;
                        for (int i = 0; i < mutari.Count; i++)
                        {
                            int[,] newBoard = mutareNoua(tabla, mutari.ElementAt(i), jucator);
                            Move m = algoritmMinimax(newBoard, depth + 1, 1, alfa, beta);
                            if (m.scor >= scorulPerfect)
                            {
                                scorulPerfect = m.scor;
                                mutareaPerfecta = mutari.ElementAt(i);
                            }
                            alfa = Math.Max(alfa, scorulPerfect);
                            if (alfa > beta)
                                break;
                        }
                        return new Move(mutareaPerfecta, scorulPerfect);
                        
                    }
                    /// daca nodul este minim
                    else
                    {
                        int scorulPerfect = int.MaxValue, mutareaPerfecta = -1000;
                        for (int i = 0; i < mutari.Count; i++)
                        {
                            int[,] aux = mutareNoua(tabla, mutari[i], jucator);
                            Move m = algoritmMinimax(aux, depth + 1, 2, alfa, beta);
                            if (m.scor <= scorulPerfect)
                            {
                                scorulPerfect = m.scor;
                                mutareaPerfecta = mutari.ElementAt(i);
                            }
                            beta = Math.Min(beta, scorulPerfect);
                            if (alfa > beta)
                                break;
                        }
                        return new Move(mutareaPerfecta, scorulPerfect);
                    }
                }
            }
        }
    }
}