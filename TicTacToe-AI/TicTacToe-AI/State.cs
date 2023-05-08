using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class State : ICloneable
    {
        public static char BLANK = ' ';
        public static char PLAYER1 = 'O';
        public static char PLAYER2 = 'X';
        public static char DRAW = 'D';
        public char[,] Board = new char[3, 3]
        {
            { BLANK, BLANK, BLANK },
            { BLANK, BLANK, BLANK },
            { BLANK, BLANK, BLANK }
        };
        public char CurrentPlayer = PLAYER1;
        public void ChangePlayer()
        {
            if (CurrentPlayer == PLAYER1)
            {
                CurrentPlayer = PLAYER2;
            }
            else
            {
                CurrentPlayer = PLAYER1;
            }
        }

        public object Clone()
        {
            State newState = new State();
            newState.Board = (char[,]) Board.Clone();
            newState.CurrentPlayer = CurrentPlayer;
            return newState;
        }

        public override bool Equals(object obj)
        {
            State other = obj as State;
            if (other.CurrentPlayer != CurrentPlayer)
                return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (other.Board[i,j] != Board[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // ha játékos karakterét adja vissza, az a játékos nyert
        // ha BLANK karakterét, akkor még lehet lépni
        // ha DRAW karakterét, akkor döntetlen
        public char GetStatus() // jobb megoldás lehet ha mondjuk külön enum-ba adjuk vissza
        {
            for (int i = 0; i < 3; i++)
            {
                // sor ellenőrzés
                if (Board[i, 2] != BLANK && 
                    Board[i, 0] == Board[i, 1] 
                    && Board[i,1] == Board[i,2])
                {
                    return Board[i,0];
                }
                // oszlop ellenőrzés
                if (Board[0, i] != BLANK &&
                    Board[0, i] == Board[1, i]
                    && Board[1, i] == Board[2, i])
                {
                    return Board[0, i];
                }
            }
            // főátló
            if (Board[0,0] != BLANK &&
                Board[0,0] == Board[1,1] &&
                Board[1,1] == Board[2,2])
            {
                return Board[0, 0];
            }
            // mellékátló
            if (Board[0, 2] != BLANK &&
                Board[0, 2] == Board[1, 1] &&
                Board[1, 1] == Board[2, 0])
            {
                return Board[0, 2];
            }
            // lehet-e még lépni
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i,j] == BLANK)
                    {
                        return Board[i, j];
                    }
                }
            }
            // végeztül döntetlen
            return DRAW;
        }


        private static int WIN = 100;
        private static int DRAW_SCORE = 50;
        private static int LOSE = -100;
        private static int POSSIBLE_WIN = 10;
        private static int POSSIBLE_LOSE = -7;
        private static int POSSIBLE_LOSE_AVOIDED = 5;
        private static int NO_WINNING_STRAT = -1;


        public int GetHeuristics(char player)
        {
            // ha már nyer akkor nagy a heurisztika
            if (GetStatus() == player)
            {
                return WIN;
            }
            if (GetStatus() == DRAW) // döntetlen jobb mint ha vesztene
            {
                return DRAW_SCORE;
            }
            if (GetStatus() != BLANK) // ha veszít, akkor az nem jó
            {
                return LOSE;
            }

            int result = 0;
            char currentPlayer;
            char otherPlayer;
            if (player == PLAYER1)
            {
                currentPlayer = PLAYER1;
                otherPlayer = PLAYER2;
            }
            else
            {
                currentPlayer = PLAYER2;
                otherPlayer = PLAYER1;
            }
            int currentCount;
            int otherCount;
            for (int i = 0; i < 3; i++)
            {
                currentCount = 0;
                otherCount = 0;
                // sorban megszámolja a jelenlegi és a másik játékos számát
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i,j] == currentPlayer)
                    {
                        currentCount++;
                    }
                    else if (Board[i,j] == otherPlayer)
                    {
                        otherCount++;
                    }
                }
                // kivédte a lehetséges vesztést
                if (currentCount == 1 && otherCount == 2)
                {
                    result += POSSIBLE_LOSE_AVOIDED;
                }
                // lehetséges győzelem
                if (currentCount == 2 && otherCount == 0)
                {
                    result += POSSIBLE_WIN;
                }
                // lehetséges vesztés
                if (otherCount == 2 && currentCount == 0)
                {
                    result += POSSIBLE_LOSE;
                }
                // nem érdemes már abba a sorba/oszlopba/átlóba rakni
                if (otherCount == 1)
                {
                    result += NO_WINNING_STRAT;
                }

                currentCount = 0;
                otherCount = 0;
                // oszlopban megszámolja a jelenlegi és a másik játékos számát
                for (int j = 0; j < 3; j++)
                {
                    if (Board[j, i] == currentPlayer)
                    {
                        currentCount++;
                    }
                    else if (Board[j, i] == otherPlayer)
                    {
                        otherCount++;
                    }
                }
                // kivédte a lehetséges vesztést
                if (currentCount == 1 && otherCount == 2)
                {
                    result += POSSIBLE_LOSE_AVOIDED;
                }
                // lehetséges győzelem
                if (currentCount == 2 && otherCount == 0)
                {
                    result += POSSIBLE_WIN;
                }
                // lehetséges vesztés
                if (otherCount == 2 && currentCount == 0)
                {
                    result += POSSIBLE_LOSE;
                }
                // nem érdemes már abba a sorba/oszlopba/átlóba rakni
                if (otherCount == 1)
                {
                    result += NO_WINNING_STRAT;
                }
            }


            // főátló heurisztikája
            currentCount = 0;
            otherCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, i] == currentPlayer)
                {
                    currentCount++;
                }
                else if (Board[i, i] == otherPlayer)
                {
                    otherCount++;
                }
            }
            // kivédte a lehetséges vesztést
            if (currentCount == 1 && otherCount == 2)
            {
                result += POSSIBLE_LOSE_AVOIDED;
            }
            // lehetséges győzelem
            if (currentCount == 2 && otherCount == 0)
            {
                result += POSSIBLE_WIN;
            }
            // lehetséges vesztés
            if (otherCount == 2 && currentCount == 0)
            {
                result += POSSIBLE_LOSE;
            }
            // nem érdemes már abba a sorba/oszlopba/átlóba rakni
            if (otherCount == 1)
            {
                result += NO_WINNING_STRAT;
            }

            // mellégátló heutisztikája
            currentCount = 0;
            otherCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 2 - i] == currentPlayer)
                {
                    currentCount++;
                }
                else if (Board[i, 2 - i] == otherPlayer)
                {
                    otherCount++;
                }
            }
            // kivédte a lehetséges vesztést
            if (currentCount == 1 && otherCount == 2)
            {
                result += POSSIBLE_LOSE_AVOIDED;
            }
            // lehetséges győzelem
            if (currentCount == 2 && otherCount == 0)
            {
                result += POSSIBLE_WIN;
            }
            // lehetséges vesztés
            if (otherCount == 2 && currentCount == 0)
            {
                result += POSSIBLE_LOSE;
            }
            // nem érdemes már abba a sorba/oszlopba/átlóba rakni
            if (otherCount == 1)
            {
                result += NO_WINNING_STRAT;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  1 2 3"); // oszlop számai
            for (int i = 0; i < 3; i++)
            {
                sb.AppendLine(" +-+-+-+");
                sb.Append(string.Format("{0}|", i+1)); // sor száma
                for (int j = 0; j < 3; j++)
                {
                    sb.Append(Board[i, j] + "|");
                }
                sb.AppendLine();
            }
            sb.AppendLine(" +-+-+-+");
            sb.AppendLine("Current player: " 
                + CurrentPlayer);
            return sb.ToString();
        }
    }
}
