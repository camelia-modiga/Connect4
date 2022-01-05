using System;
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
        public const int PLAYER = 1;
        public const int COMPUTER = 2;
        public const int DRAW = 3;
        public readonly int L;
        public readonly int C;
        public HashSet<String> increment;
        public HashSet<String> decrement;
   //     public Dictionary<string, int> visited;
        public int[,] tabla;
        private int latime;
        private int inaltime;
        private int latimeBila;
        private int inaltimeBila;

        public int raza { get; set; }
        Color jucator;
        Color calculator;

        public Connect(int L, int C, int inaltimePanel, int latimePanel, Color j, Color c)
        {
            jucator = j;
            calculator = c;
            string[] a = { "0002", "0020", "0022", "0200", "0202", "0220", "0222", "2000", "2002", "2020", "2022", "2200", "2202", "2220", "2222" };
            increment = new HashSet<String>(a);
            string[] b = { "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };
            decrement = new HashSet<String>(b);
            tabla = new int[L, C];
            this.L= L;
            this.C = C;

            this.inaltime = inaltimePanel ;
            this.latime = latimePanel;
            latimeBila = latime / C;
            inaltimeBila = inaltime / L;
            raza = Math.Min(latimeBila, inaltimeBila) / 2;
            raza -= 5;
        }

        public void printBoard(Graphics graphics)
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
                    int x = (j * latimeBila) + (latimeBila / 2) - raza;
                    int y = (i * inaltimeBila) + (inaltimeBila / 2) - raza;
                    graphics.FillEllipse(brush, x, y, 2 * raza, 2 * raza);
                    brush.Dispose();
                }
            }
        }

        // Verifică dacă există un câștigător în starea actuală și îl returnează dacă există
        public int checkWinner()
        {
            int flag = 1;
            for (int i = 0; i < L; ++i)
                for (int j = 0; j < C; ++j)
                    if (tabla[i, j] == 0)
                        flag = 0;

            if (flag == 1) 
                return 3;

            return checkActualWinner(tabla);
        }

        public int checkActualWinner(int[,] tabla)
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

        public bool playerMove(Point p)
        {
            int alegere = p.X / latimeBila;
            if (!validMoves(tabla).Contains(alegere))
                return false;
            // Reprezintă alegerea de mutare a jucătorului
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

        public void computerMove()
        {
            Move m = minimax(tabla, 0, 2, int.MinValue, int.MaxValue);

            if (m.mutare != -1 && m.mutare != -2)
            {
                for (int i = L - 1; i >= 0; i--)
                {
                    if (tabla[i, m.mutare] == 0)
                    {
                        tabla[i, m.mutare] = 2;
                        break;
                    }
                }
            }
        }

        // Returnează coloanele care nu se ocupă
        private List<int> validMoves(int[,] tabla)
        {
            List<int> mutari = new List<int>();
            for (int i = 0; i < C; i++)
                if (tabla[0, i] == 0) 
                    mutari.Add(i);
            return mutari;
        }

        // Funcție ce primește starea curentă, ce jucător a făcut mutarea și în ce coloană
        // Returnează primul spațiu liber din acea coloană
        private int[,] applyMove(int[,] tabla, int column, int jucator)
        {
            int[,] newBoard = new int[L,C];
            for (int i = 0; i < L; i++)
                for (int j = 0; j < C; j++)
                    newBoard[i, j] = tabla[i, j];

            for (int i = L - 1; i >= 0; i--)
            {
                if (newBoard[i, column] == 0)
                {
                    newBoard[i, column] = jucator;
                    break;
                }
            }
            return newBoard;
        }

        // Algoritumul minimax cu alfa beta pruning
        public Move minimax(int[,] tabla, int depth, int jucator, int a, int b)
        {
            int alpha = a;
            int beta = b;
            int castigator = checkActualWinner(tabla);
            if (depth == 3)
                return new Move(-1, getHeuristic(tabla));
            else if (castigator != -1)
                return new Move(-1, castigator == 1 ? -1 * int.MaxValue : int.MaxValue);
            else
            {
                List<int> mutari = validMoves(tabla);
                if (mutari.Count == 0)
                    return new Move(-2, getHeuristic(tabla));
                else
                {
                    // Dacă nodul e minim
                    if (jucator == 1)
                    {
                        int scorulPerfect = int.MaxValue, mutareaPerfecta = -1;
                        for (int i = 0; i < mutari.Count; i++)
                        {
                            int[,] newBoard = applyMove(tabla, mutari[i], jucator);
                            Move m = minimax(newBoard, depth + 1, 2, alpha, beta);
                            if (m.scor <= scorulPerfect)
                            {
                                scorulPerfect = m.scor;
                                mutareaPerfecta = mutari.ElementAt(i);
                            }
                            beta = Math.Min(beta, scorulPerfect);
                            if (beta < alpha) break;
                        }
                        return new Move(mutareaPerfecta, scorulPerfect);
                    }
                    // Dacă nodul e maxim
                    else
                    {
                        int scorulPerfect = int.MinValue, mutareaPerfecta = -1;
                        for (int i = 0; i < mutari.Count; i++)
                        {
                            int[,] newBoard = applyMove(tabla, mutari.ElementAt(i), jucator);
                            Move m = minimax(newBoard, depth + 1, 1, alpha, beta);
                            if (m.scor >= scorulPerfect)
                            {
                                scorulPerfect = m.scor;
                                mutareaPerfecta = mutari.ElementAt(i);
                            }
                            alpha = Math.Max(alpha, scorulPerfect);
                            if (beta < alpha) break;
                        }
                        return new Move(mutareaPerfecta, scorulPerfect);
                    }
                }
            }
        }

        // Funcția de evaluare
        public int getHeuristic(int[,] tabla)
        {
            int castigator = checkActualWinner(tabla);
            string s;
            if (castigator != -1)
            {
                int m = castigator == 1 ? -1 : 1;
                return m * int.MaxValue;
            }
            int nr = 0;
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j <= C - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i, j + 1], tabla[i, j + 2], tabla[i, j + 3]);
                    if (increment.Contains(s))
                        nr++;
                    else if (decrement.Contains(s))
                        nr--;
                }
            }
            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j], tabla[i + 2, j], tabla[i + 3, j]);
                    if (increment.Contains(s))
                        nr++;
                    else if (decrement.Contains(s))
                        nr--;
                }
            }
            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 0; j <= C - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j + 1], tabla[i + 2, j + 2], tabla[i + 3, j + 3]);
                    if (increment.Contains(s))
                        nr++;
                    else if (decrement.Contains(s))
                        nr--;
                }
            }

            for (int i = 0; i <= L - 4; i++)
            {
                for (int j = 3; j < C; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", tabla[i, j], tabla[i + 1, j - 1], tabla[i + 2, j - 2], tabla[i + 3, j - 3]);
                    if (increment.Contains(s))
                        nr++;
                    else if (decrement.Contains(s))
                        nr--;
                }
            }
            return nr;
        }
    }
}