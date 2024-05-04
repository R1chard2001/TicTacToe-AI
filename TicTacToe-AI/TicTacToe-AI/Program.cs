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
            Game game = new Game(new MiniMaxABPruneSolver(5, false));
            game.Play();
            Console.ReadLine();
        }
    }
}
