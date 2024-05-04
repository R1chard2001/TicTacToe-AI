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
        public bool HasBeenExtended = false;
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


        public void SortChildrenMinimax(char currentPlayer, bool isCurrentPlayer = true)
        {
            foreach (Node node in Children)
            {
                node.SortChildrenMinimax(currentPlayer, !isCurrentPlayer);
            }
            if (isCurrentPlayer) // ha a jelenlegi játékos lép (a gép), akkor csökkenő sorrend
            {
                Children.Sort((x, y) => y.GetHeuristics(currentPlayer).CompareTo(x.GetHeuristics(currentPlayer)));
            }
            else // ha a másik játékos, akkor növekvő sorrendbe rakjuk a gyerekelemek listáját
            {
                Children.Sort((x, y) => x.GetHeuristics(currentPlayer).CompareTo(y.GetHeuristics(currentPlayer)));
            }
        }
        // heurisztikát a levélelemekből számítunk, majd felfele haladva a fán, a gyerekelemekből
        // ha a következő lépésben (lejjebb lévő szint) a jelenlegi játékos (gép) jönne, akkor maximumkeresés
        // ha a másik játékos jönne, akkor pedig minimumkeresést alkalmazunk
        // megj.: előre rendezett listánál visszaadhatjuk az első, illetve utolsó elemet
        //        de mivel alapból növekvő majd csökkenő sorrendbe rendezzük a gyermekek listáját szintenkéne
        //        ezért ezt nem kell megtenni
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
