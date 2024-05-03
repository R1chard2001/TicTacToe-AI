using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class MiniMaxABPruneSolver : ASolver
    {
        public MiniMaxABPruneSolver(int depth) : base()
        {
            Depth = depth;
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
                return getAPossibleNextState(currentState);
            return currentNode.Children[0].State;
        }
        public State getAPossibleNextState(State state)
        {   
            // ha a vágások miatt nem maradna gyerekelem, akkor adja vissza a legelső valós állapotot
            foreach (Operator op in Operators)
                if (op.IsApplicable(state))
                    return op.Apply(state);
            throw new Exception("The AI can't move!");
        }

        private void extendNode(Node node)
        {
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
                    extendNode(newNode);
                }
            }
        }
        private void BetaPrune(Node node)
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
