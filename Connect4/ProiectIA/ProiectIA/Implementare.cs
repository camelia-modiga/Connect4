using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ProiectIA
{
    class Implementare
    {
        private const int L = 6;
        private const int C = 7;
        
        public const int JUCATOR = 1;
        public const int CALCULATOR = 2;
        public const int REMIZA = 3;

        private int[,] tabla;
        
        // lista reprezinta bilele calculatorului pozitionate pe tabla la un anumit moment
        private List<string> mutariCalculator = new List<string>(new[]
        {
            "0002", "0020", "0022", "0200", "0202", "0220", "0222", "2000", "2002", "2020", "2022", "2200", "2202",
            "2220", "2222"
        });
        
        // lista reprezinta bilele jucatorului pozitionate pe tabla la un anumit moment
        private List<string> mutariJucator = new List<string>(new []
        {
            "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101",
            "1110", "1111"
        });

        private int latime;
        private int inaltime;
        private int raza { get;}
        
        Color jucator;
        Color calculator;

        public Implementare(int inaltimePanou, int latimePanou)
        {
            tabla = new int[L, C];
            jucator = Color.Yellow;
            calculator = Color.Red;
            this.inaltime = inaltimePanou;
            this.latime = latimePanou;
            raza = Math.Min(latime / C, inaltime / L) / 2 - 5;
        }

        /// <summary>
        /// Metoda folosita pentru desenarea tablei de joc
        /// </summary>
        /// <param name="graphics"></param>
        public void deseneazaTabla(Graphics graphics)
        {
            graphics.Clear(Color.DarkBlue);
            for (var i = 0; i < L; ++i)
            {
                for (var j = 0; j < C; ++j)
                {
                    Color color = Color.Black;
                    // la fiecare mutare se redeseneaza toate spatiile pe tabla chiar daca 
                    // sunt goale sau ocupate de bile, conform datelor memorate in vectorul tabla care
                    // se reactualizeaza la fiecare mutare
                    if (tabla[i, j] == 1)
                        color = jucator;
                    if (tabla[i, j] == 2)
                        color = calculator;
                    Brush brush = new SolidBrush(color);
                    var bilaX = (j * (latime / C)) + ((latime / C) / 2) - raza;
                    var bilaY = (i * (inaltime / L)) + ((inaltime / L) / 2) - raza;
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
        private int verificaCastigator(int[,] tabla)
        {
            // verifica daca exista 4 bile egale pe orizontala
            for (var i = 0; i < L; i++)
                for (var j = 0; j <= C - 4; j++)
                    if (tabla[i, j] != 0 && tabla[i, j + 1] == tabla[i, j] && tabla[i, j + 2] == tabla[i, j] &&
                        tabla[i, j + 3] == tabla[i, j])
                        return tabla[i, j];
            
            // verifica daca exista 4 bile egale pe diagonala spre dreapa (\)
            for (var i = 0; i <= L - 4; i++)
                for (var j = 0; j <= C - 4; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j + 1] == tabla[i, j] && tabla[i + 2, j + 2] == tabla[i, j] &&
                        tabla[i + 3, j + 3] == tabla[i, j])
                        return tabla[i, j];

            // verifica daca exista 4 bile egale pe vericala
            for (var i = 0; i <= L - 4; i++)
                for (var j = 0; j < C; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j] == tabla[i, j] && tabla[i + 2, j] == tabla[i, j] &&
                        tabla[i + 3, j] == tabla[i, j])
                        return tabla[i, j];
            
            // verifica daca exista 4 bile egale pe diagonala spre stanga (/)
            for (var i = 0; i <= L - 4; i++)
                for (var j = 3; j < C; j++)
                    if (tabla[i, j] != 0 && tabla[i + 1, j - 1] == tabla[i, j] && tabla[i + 2, j - 2] == tabla[i, j] &&
                        tabla[i + 3, j - 3] == tabla[i, j])
                        return tabla[i, j];
            return -1;
        }

        /// <summary>
        /// Metoda care verifica daca in starea actuala exista un castigator si il returneaza daca exista 
        /// </summary>
        /// <returns></returns>
        public int stareCurenta()
        {
            var aux = 1;
            // se verifica daca exista spatii goale care pot fi completate
            for (var i = 0; i < L; ++i)
                for (var j = 0; j < C; ++j)
                    if (tabla[i, j] == 0)
                        aux = 0;
            // daca nu mai exista spatii goale si nu au fost conectate 4 bile de aceeasi culoare
            // nu avem un castigator 
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
            // reprezinta pozitia unde urmeaza sa fie asezata bila
            var alegere = locatie.X / (latime / C);
            
            if (!mutareValida(tabla).Contains(alegere))
                return false;
            
            // asezam bila pe primul spatiu liber din coloana aleasa
            for (var i = L - 1; i >= 0; i--)
            {
                if (tabla[i, alegere] == 0)
                {
                    tabla[i, alegere] = 1;
                    break;
                }
            }
            return true;
        }

        /// <summary>
        /// Metoda care calculeaza urmatoarea pozitie a calculatorului
        /// </summary>
        public void mutareCalculator()
        {
            // algoritmul Minimax se apeleaza dupa fiecare mutare a jucatorului
            var mutare = algoritmMinimax(tabla, 0, 2, int.MinValue, int.MaxValue);

            if (mutare.Item1 != -1 && mutare.Item1 != -2)
            {
                for (var i = L - 1; i >= 0; --i)
                {
                    // daca exista spatiu pe tabla, atunci se pune piesa (pe primul loc liber)
                    if (tabla[i, mutare.Item1] == 0)
                    {
                        tabla[i, mutare.Item1] = 2;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Metoda care returneaza coloanele pe care mai sunt acceptate mutari
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        private List<int> mutareValida(int[,] tabla)
        {
            var mutariValide = new List<int>();
            // daca exita o pozitile libera pe tabla de joc ea reprezinta o mutare valida
            for (var i = 0; i < C; i++)
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
            var aux = new int[L, C];
            
            // se face o copie a tablei de joc cu scopul prelucrarii acesteia
            for (var i = 0; i < L; i++)
                for (var j = 0; j < C; j++)
                    aux[i, j] = tabla[i, j];
            
            // daca exista o poztie libera pe tabla de joc aceasta va fi ocupata de bila jucatorului
            for (var i = L - 1; i >= 0; i--)
            {
                if (aux[i, coloana] == 0)
                {
                    aux[i, coloana] = jucator;
                    break;
                }
            }
            return aux;
        }
        
        /// <summary>
        /// Metoda care calculeaza ponderile nodurilor 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="nr"></param>
        /// <returns></returns>
        private int calculPondere(string s, int nr)
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
        private int functieEvaluare(int[,] tabla)
        {
            var castigator = verificaCastigator(tabla);
            string s;
            // daca avem un castigator
            if (castigator != -1)
            {
                // si castigatorul este jucatorul 
                if (castigator == 1)
                    return int.MinValue;
                else
                    return int.MaxValue;
            }

            var nr = 0;
            
            // calculeaza ponderea pe orizontala
            for (var i = 0; i < L; i++)
            {
                for (var j = 0; j <= C - 4; j++)
                {
                    s = $"{tabla[i, j]}{tabla[i, j + 1]}{tabla[i, j + 2]}{tabla[i, j + 3]}";
                    nr = calculPondere(s, nr);
                }
            }
            
            // calculeaza ponderea pe verticala
            for (var i = 0; i <= L - 4; i++)
            {
                for (var j = 0; j < C; j++)
                {
                    s = $"{tabla[i, j]}{tabla[i + 1, j]}{tabla[i + 2, j]}{tabla[i + 3, j]}";
                    nr = calculPondere(s, nr);
                }
            }

            // calculeaza ponderea pe diagonala spre dreapta (\)
            for (var i = 0; i <= L - 4; i++)
            {
                for (var j = 0; j <= C - 4; j++)
                {
                    s = $"{tabla[i, j]}{tabla[i + 1, j + 1]}{tabla[i + 2, j + 2]}{tabla[i + 3, j + 3]}";
                    nr = calculPondere(s, nr);
                }
            }
            
            // calculeaza ponderea pe diagonala spre stanga (/)
            for (var i = 0; i <= L - 4; i++)
            {
                for (var j = 3; j < C; j++)
                {
                    s = $"{tabla[i, j]}{tabla[i + 1, j - 1]}{tabla[i + 2, j - 2]}{tabla[i + 3, j - 3]}";
                    nr = calculPondere(s, nr);
                }
            }
            return nr;
        }

        /// <summary>
        /// Algoritumul minimax cu retezare alfa beta
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="adancime"></param>
        /// <param name="jucator"></param>
        /// <param name="alfa"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        private (int, int) algoritmMinimax(int[,] tabla, int adancime, int utilizator, int alfa, int beta)
        {
            var castigator = verificaCastigator(tabla);

            // adancimea arborelui este 3
            // conditia de terminare
            // am ajuns la nodul frunza
            if (adancime == 3)
                return (-1, functieEvaluare(tabla));
            // daca exista un castigator
            if (castigator != -1)
                // si castigatorul este jucatorul
                if (castigator == 1)
                    // jucatorul devine minimzant
                    return (-1, int.MinValue);
                else
                    // altfel devine maximinzant
                    return (-1, int.MaxValue);
            var mutari = mutareValida(tabla);
            // daca nu mai avem pozitii libere pe tabla de joc se retureneaza -2
            if (mutari.Count == 0)
                return (-2, functieEvaluare(tabla));
            // daca nodul este maxim
            // adica utilizatorul este reprezentat de calculator
            if (utilizator !=1)
            {
                //valoarea initiala pentru alfa
                var scorOptim = int.MinValue;
                var mutareOptima = -1000;
                        
                for (var i = 0; i < mutari.Count; i++)
                {
                    var aux = mutareNoua(tabla, mutari[i], utilizator);
                    var mutare = algoritmMinimax(aux, adancime + 1, 1, alfa, beta);
                            
                    if (mutare.Item2 >= scorOptim)
                    {
                        scorOptim = mutare.Item2;
                        mutareOptima = mutari[i];
                    }

                    alfa = Math.Max(alfa, scorOptim);
                            
                    // Condiția necesară pentru retezarea alfa-beta
                    if (alfa > beta)
                        break;
                }

                return (mutareOptima, scorOptim);

            }
            // daca nodul este minim
            // adica utilizatorul este reprezentat de jucator
            else
            {
                //valoarea initiala pentru beta
                var scorOptim = int.MaxValue;
                var mutareOptima = -1000;
                        
                for (var i = 0; i < mutari.Count; i++)
                {
                    var aux = mutareNoua(tabla, mutari[i], utilizator);
                    var mutare = algoritmMinimax(aux, adancime + 1, 2, alfa, beta);
                           
                    if (mutare.Item2 <= scorOptim)
                    {
                        scorOptim = mutare.Item2;
                        mutareOptima = mutari[i];
                    }

                    beta = Math.Min(beta, scorOptim);
                            
                    // Condiția necesară pentru retezarea alfa-beta
                    if (alfa > beta)
                        break;
                }
                return (mutareOptima, scorOptim);
            }
        }
    }
}
