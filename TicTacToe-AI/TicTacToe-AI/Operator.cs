using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI
{
    public class Operator
    {
        public int Row;
        public int Col;
        public Operator(int row, int col)
        {
            Row = row;
            Col = col;
        }
        public bool IsAplicable(State state)
        {
            return state.Board[Row, Col] == State.BLANK;
        }
        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            newState.Board[Row, Col] = state.CurrentPlayer;
            newState.ChangePlayer();
            return newState;
        }
    }
}
