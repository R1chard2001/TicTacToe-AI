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
