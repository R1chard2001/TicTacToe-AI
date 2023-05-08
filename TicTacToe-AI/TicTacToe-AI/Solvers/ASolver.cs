using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    internal abstract class ASolver
    {
        public List<Operator> Operators = new List<Operator>();
        public ASolver()
        {
            generateOperators();
        }
        private void generateOperators()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Operators.Add(new Operator(i,j));
                }
            }
        }
        public abstract State NextMove(State currentState);
    }
}
