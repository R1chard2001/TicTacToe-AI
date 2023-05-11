using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class HeuristicSolver : ASolver
    {
        public override State NextMove(State currentState)
        {
            List<State> states = new List<State>();
            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(currentState))
                {
                    states.Add(op.Apply(currentState));
                }
            }
            char p = currentState.CurrentPlayer;
            states.Sort((x, y) => y.GetHeuristics(p).CompareTo(x.GetHeuristics(p)));
            return states[0];
        }
    }
}
