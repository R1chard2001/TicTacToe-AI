using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    internal class Game
    {
        private ASolver solver;

        public Game(ASolver solver)
        {
            this.solver = solver;
        }

        public void Play()
        {
            State currentState = new State();
            bool playersTurn = true;
            while (currentState.GetStatus() == State.BLANK)
            {
                Console.WriteLine(currentState);
                if (playersTurn)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                playersTurn = !playersTurn;
            }
            Console.WriteLine(currentState);
            char status = currentState.GetStatus();
            if (status == State.DRAW)
            {
                Console.WriteLine("Draw");
            }
            else
            {
                Console.WriteLine("Winner: " + status);
            }
        }
        private State PlayersMove(State currentState)
        {
            Operator op = null;
            while (op == null || !op.IsApplicable(currentState))
            {
                int row;
                int col;
                do
                {
                    Console.Write("Row: ");
                } while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 3);
                do
                {
                    Console.Write("Column: ");
                } while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 3);
                row--;
                col--;
                op = new Operator(row, col);
            }
            return op.Apply(currentState);
        }

        private State AIsMove(State currentState)
        {
            State nextState = solver.NextMove(currentState);
            if (nextState == null)
            {
                throw new Exception("Cannot select next move.");
            }
            return nextState;
        }
    }
}
