
using System.Collections.Generic;

namespace Conn4_WPF
{
    public abstract class AbstractBoard
    {
        public class CommonMove
        {
            public int move_idx;
        }

        abstract public List<CommonMove> validMoves();

       //
       // Returns true if move was a winning one
       //
       public struct MoveResult
       {
           public bool was_winning_move;
           public bool was_invalid_move;

           public MoveResult(bool winning_move, bool invalid_move)
           {
               was_winning_move = winning_move;
	           was_invalid_move = invalid_move;
           }

       }
   
       abstract public MoveResult makeMove(CommonMove move);

       abstract public void makeBestMove();

       abstract public int rateBoard();

       public bool blackToMove;
      }    

}