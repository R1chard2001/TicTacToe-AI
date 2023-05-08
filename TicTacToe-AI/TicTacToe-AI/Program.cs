using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    internal class Program
    {

        static void Main(string[] args)
        {
            /*
            State CurrentState = new State();
            while (CurrentState.GetStatus() == '_')
            {
                int row;
                int col;
                Console.WriteLine(CurrentState);
                do
                {
                    Console.Write("Row: ");
                } while (!int.TryParse(
                    Console.ReadLine(),
                    out row)
                );
                do
                {
                    Console.Write("Col: ");
                } while (!int.TryParse(
                    Console.ReadLine(),
                    out col)
                );
                Operator op = new Operator(row, col);
                if (op.IsAplicable(CurrentState))
                {
                    CurrentState = op.Apply(CurrentState);
                }
            }
            Console.WriteLine(CurrentState);
            Console.WriteLine("Winner: " +
                CurrentState.GetStatus());*/

            Game game = new Game(new TrialAndError());
            game.Play();
            Console.ReadLine();
        }
    }
}
