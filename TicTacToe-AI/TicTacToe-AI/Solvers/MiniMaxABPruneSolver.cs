using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class MiniMaxABPruneSolver : ASolver
    {
        Action<Node> BetaPrune;
        public MiniMaxABPruneSolver(int depth, bool useSimpleBetaPrune = true) : base()
        {
            Depth = depth;
            if (useSimpleBetaPrune)
                BetaPrune = BetaPruneSimple;
            else
                BetaPrune = BetaPruneFull;
        }
        public int Depth;

        char player;
        char otherPlayer;
        public override State NextMove(State currentState)
        {
            player = currentState.CurrentPlayer;
            otherPlayer = player == State.PLAYER1 ? State.PLAYER2 : State.PLAYER1;
            Node currentNode = new Node(currentState);
            extendNode(currentNode);
            currentNode.SortChildrenMinimax(currentState.CurrentPlayer);
            if (currentNode.Children.Count == 0)
                foreach (Operator op in Operators)
                    if (op.IsApplicable(currentState))
                        return op.Apply(currentState);
            return currentNode.Children[0].State;
        }

        private void extendNode(Node node)
        {
            node.HasBeenExtended = true;
            if (node.GetStatus() != State.BLANK || node.Depth >= Depth) return;

            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(node.State))
                {
                    State newState = op.Apply(node.State);
                    char status = newState.GetStatus();
                    if (status == otherPlayer) // lépésünk miatt nyer az ellenfél
                    {
                        BetaPrune(node); // ne feltételezzük, hogy az ellenfél rossz lépést tesz, nem érdemes a többit nézni
                        return;
                    }
                    Node newNode = new Node(newState, node);
                    if (status == player) // ha mi nyerünk, több opciót nem kell nézni
                    {
                        AlphaPrune(newNode);
                        return;
                    }
                    node.Children.Add(newNode);
                }
            }
            Node childNode = getChildNodeToExtend(node);
            while (childNode != null)
            {
                extendNode(childNode);
                childNode = getChildNodeToExtend(node);
            }
        }
        private Node getChildNodeToExtend(Node parent)
        {
            return parent.Children.Find(x => !x.HasBeenExtended);
        }
        private void BetaPruneFull(Node node)
        {
            // "vágjuk ki" a lépésünk általi legnagyobb lehetséges ágat
            while (node != null && node.Parent != null)
            {
                // node -> mi lépésünk
                // parent -> ellenfél lépése
                node.Children.Clear();
                Node parent = node.Parent;
                parent.Children.Remove(node); // eltávolítjuk a rossz lépést
                if (parent.Children.Count > 0) // van-e másik lehetőségünk?
                    return;
                // ha nincs, folytassuk a vágást
                node = parent.Parent;
            }
        }
        private void BetaPruneSimple(Node node)
        {
            // "vágjuk ki" a lépésünk általi ágat
            node.Children.Clear();
            Node temp = node.Parent;
            if (temp == null)
                return;
            temp.Children.Remove(node);
        }
        private void AlphaPrune(Node node)
        {
            // többi ág nem kell, "kivághatjuk" azokat
            Node parent = node.Parent;
            if (parent == null) return;
            parent.Children.Clear();
            parent.Children.Add(node);
        }
    }
}
