using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    internal class Node
    {
        public State State;
        public int Depth;
        public Node Parent;
        public List<Node> Children = new List<Node>();
        public int OperatorIndex;
        public Node(State state, Node parent = null)
        {
            Parent = parent;
            State = state;
            OperatorIndex = 0;
            if (Parent == null)
            {
                Depth = 0;
            }
            else
            {
                Depth = Parent.Depth + 1;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Node)) return false;
            Node other = obj as Node;
            return State.Equals(other.State);
        }
        public char GetStatus()
        {
            return State.GetStatus();
        }


        public void SortChildren(char currentPlayer, bool isCurrentPlayer = true)
        {
            foreach (Node node in Children)
            {
                node.SortChildren(currentPlayer, !isCurrentPlayer);
            }
            if (isCurrentPlayer)
            {
                Children.Sort((x, y) => y.GetHeuristics(currentPlayer).CompareTo(x.GetHeuristics(currentPlayer)));
            }
            else
            {
                Children.Sort((x, y) => x.GetHeuristics(currentPlayer).CompareTo(y.GetHeuristics(currentPlayer)));
            }
        }
        public int GetHeuristics(char currentPlayer)
        {
            if (Children.Count == 0)
            {
                return State.GetHeuristics(currentPlayer);
            }
            return Children[0].GetHeuristics(currentPlayer);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Parent != null)
            {
                sb.AppendLine(Parent.ToString());
                sb.AppendLine("---------------------");
            }
            sb.AppendLine("Depth: " + Depth);
            sb.Append(State.ToString());
            return sb.ToString();
        }

        public bool HasLoop()
        {
            Node temp = Parent;
            while (temp != null)
            {
                if (temp.Equals(this))
                {
                    return true;
                }
                temp = temp.Parent;
            }
            return false;
        }
    }
}
