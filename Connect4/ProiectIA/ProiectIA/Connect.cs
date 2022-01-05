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
        public int move;
        public int score;

        public Move(int m, int s)
        {
            move = m;
            score = s;
        }
    }

    class Connect
    {
        public const int PLAYER = 1;
        public const int COMPUTER = 2;
        public const int DRAW = 3;
        public readonly int N;
        public readonly int M;
        public HashSet<String> increment;
        public HashSet<String> decrement;
        public Dictionary<string, int> visited;
        public int[,] board;
        private int panelWidth;
        private int panelHeight;
        private int circleWidth;
        private int circleHeight;

        public int Radius { get; set; }
        Color player1;
        Color player2;

        public Connect(int N, int M, int panelHeight, int panelWidth, Color p1, Color p2)
        {
            player1 = p1;
            player2 = p2;
            string[] a = { "0002", "0020", "0022", "0200", "0202", "0220", "0222", "2000", "2002", "2020", "2022", "2200", "2202", "2220", "2222" };
            increment = new HashSet<String>(a);
            string[] b = { "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };
            decrement = new HashSet<String>(b);
            board = new int[N, M];
            this.N = N;
            this.M = M;

            this.panelHeight = panelHeight;
            this.panelWidth = panelWidth;
            circleWidth = panelWidth / M;
            circleHeight = panelHeight / N;
            Radius = Math.Min(circleWidth, circleHeight) / 2;
            Radius -= 5;
        }

        public void printBoard(Graphics g)
        {
            g.Clear(Color.Blue);
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    Color color = Color.White;
                    if (board[i, j] == 1)
                        color = player1;
                    if (board[i, j] == 2)
                        color = player2;
                    Brush brush = new SolidBrush(color);
                    int x = (j * circleWidth) + (circleWidth / 2) - Radius;
                    int y = (i * circleHeight) + (circleHeight / 2) - Radius;
                    g.FillEllipse(brush, x, y, 2 * Radius, 2 * Radius);
                    brush.Dispose();
                }
            }
        }

        // Verifică dacă există un câștigător în starea actuală și îl returnează dacă există
        public int checkWinner()
        {
            int flag = 1;
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < M; ++j)
                    if (board[i, j] == 0)
                        flag = 0;

            if (flag == 1) 
                return 3;

            return checkActualWinner(board);
        }

        public int checkActualWinner(int[,] board)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j <= M - 4; j++)
                    if (board[i, j] != 0 && board[i, j + 1] == board[i, j] && board[i, j + 2] == board[i, j] && board[i, j + 3] == board[i, j])
                        return board[i, j];

            for (int i = 0; i <= N - 4; i++)
                for (int j = 0; j < M; j++)
                    if (board[i, j] != 0 && board[i + 1, j] == board[i, j] && board[i + 2, j] == board[i, j] && board[i + 3, j] == board[i, j])
                        return board[i, j];

            for (int i = 0; i <= N - 4; i++)
                for (int j = 0; j <= M - 4; j++)
                    if (board[i, j] != 0 && board[i + 1, j + 1] == board[i, j] && board[i + 2, j + 2] == board[i, j] && board[i + 3, j + 3] == board[i, j])
                        return board[i, j];

            for (int i = 0; i <= N - 4; i++)
                for (int j = 3; j < M; j++)
                    if (board[i, j] != 0 && board[i + 1, j - 1] == board[i, j] && board[i + 2, j - 2] == board[i, j] && board[i + 3, j - 3] == board[i, j])
                        return board[i, j];
            return -1;

        }

        public bool playerMove(Point p)
        {
            int choice = p.X / circleWidth;
            if (!validMoves(board).Contains(choice))
                return false;
            // Reprezintă alegerea de mutare a jucătorului
            for (int i = N - 1; i >= 0; i--)
            {
                if (board[i, choice] == 0)
                {
                    board[i, choice] = 1;
                    break;
                }
            }
            return true;
        }

        public void computerMove()
        {
            Move m = minimax(board, 0, 2, int.MinValue, int.MaxValue);

            if (m.move != -1 && m.move != -2)
            {
                for (int i = N - 1; i >= 0; i--)
                {
                    if (board[i, m.move] == 0)
                    {
                        board[i, m.move] = 2;
                        break;
                    }
                }
            }
        }

        // Returnează coloanele care nu se ocupă
        private List<int> validMoves(int[,] board)
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < M; i++)
                if (board[0, i] == 0) 
                    moves.Add(i);
            return moves;
        }

        // Funcție ce primește starea curentă, ce jucător a făcut mutarea și în ce coloană
        // Returnează primul spațiu liber din acea coloană
        private int[,] applyMove(int[,] board, int column, int player)
        {
            int[,] newBoard = new int[N, M];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    newBoard[i, j] = board[i, j];

            for (int i = N - 1; i >= 0; i--)
            {
                if (newBoard[i, column] == 0)
                {
                    newBoard[i, column] = player;
                    break;
                }
            }
            return newBoard;
        }

        // Algoritumul minimax cu alfa beta pruning
        public Move minimax(int[,] board, int depth, int player, int a, int b)
        {
            int alpha = a;
            int beta = b;
            int winner = checkActualWinner(board);
            if (depth == 3)
                return new Move(-1, getHeuristic(board));
            else if (winner != -1)
                return new Move(-1, winner == 1 ? -1 * int.MaxValue : int.MaxValue);
            else
            {
                List<int> moves = validMoves(board);
                if (moves.Count == 0)
                    return new Move(-2, getHeuristic(board));
                else
                {
                    // Dacă nodul e minim
                    if (player == 1)
                    {
                        int best_score = int.MaxValue, best_move = -1;
                        for (int i = 0; i < moves.Count; i++)
                        {
                            int[,] newBoard = applyMove(board, moves[i], player);
                            Move m = minimax(newBoard, depth + 1, 2, alpha, beta);
                            if (m.score <= best_score)
                            {
                                best_score = m.score;
                                best_move = moves.ElementAt(i);
                            }
                            beta = Math.Min(beta, best_score);
                            if (beta < alpha) break;
                        }
                        return new Move(best_move, best_score);
                    }
                    // Dacă nodul e maxim
                    else
                    {
                        int best_score = int.MinValue, best_move = -1;
                        for (int i = 0; i < moves.Count; i++)
                        {
                            int[,] newBoard = applyMove(board, moves.ElementAt(i), player);
                            Move m = minimax(newBoard, depth + 1, 1, alpha, beta);
                            if (m.score >= best_score)
                            {
                                best_score = m.score;
                                best_move = moves.ElementAt(i);
                            }
                            alpha = Math.Max(alpha, best_score);
                            if (beta < alpha) break;
                        }
                        return new Move(best_move, best_score);
                    }
                }
            }
        }

        // Funcția de evaluare
        public int getHeuristic(int[,] board)
        {
            int winner = checkActualWinner(board);
            string s;
            if (winner != -1)
            {
                int m = winner == 1 ? -1 : 1;
                return m * int.MaxValue;
            }
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= M - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", board[i, j], board[i, j + 1], board[i, j + 2], board[i, j + 3]);
                    if (increment.Contains(s))
                        count++;
                    else if (decrement.Contains(s))
                        count--;
                }
            }
            for (int i = 0; i <= N - 4; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", board[i, j], board[i + 1, j], board[i + 2, j], board[i + 3, j]);
                    if (increment.Contains(s))
                        count++;
                    else if (decrement.Contains(s))
                        count--;
                }
            }
            for (int i = 0; i <= N - 4; i++)
            {
                for (int j = 0; j <= M - 4; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", board[i, j], board[i + 1, j + 1], board[i + 2, j + 2], board[i + 3, j + 3]);
                    if (increment.Contains(s))
                        count++;
                    else if (decrement.Contains(s))
                        count--;
                }
            }

            for (int i = 0; i <= N - 4; i++)
            {
                for (int j = 3; j < M; j++)
                {
                    s = String.Format("{0}{1}{2}{3}", board[i, j], board[i + 1, j - 1], board[i + 2, j - 2], board[i + 3, j - 3]);
                    if (increment.Contains(s))
                        count++;
                    else if (decrement.Contains(s))
                        count--;
                }
            }
            return count;
        }
    }
}