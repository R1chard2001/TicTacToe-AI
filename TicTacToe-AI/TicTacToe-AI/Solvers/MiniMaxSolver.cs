using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class MiniMaxSolver : ASolver
    {
        public MiniMaxSolver(int depth) : base()
        {
            Depth = depth;
        }
        public int Depth;


        public override State NextMove(State currentState)
        {
            Node currentNode = new Node(currentState);
            extendNode(currentNode);
            currentNode.SortChildrenMinimax(currentState.CurrentPlayer);
            return currentNode.Children[0].State;
        }

        private void extendNode(Node node)
        {
            if (node.GetStatus() != State.BLANK || node.Depth >= Depth) return;
            
            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(node.State))
                {
                    State newState = op.Apply(node.State);
                    Node newNode = new Node(newState, node);
                    node.Children.Add(newNode);
                    extendNode(newNode);
                }
            }
        }
    }
}
