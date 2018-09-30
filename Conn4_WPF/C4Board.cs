
using System;
using System.Collections.Generic;
using System.Text;

namespace Conn4_WPF
{
    public class C4Board : AbstractBoard
    {
    	public class C4_Move : CommonMove
	    {
	        public int col_idx { get; set;}
    	}

	    public enum PieceType {BLACK, WHITE, EMPTY};

    	public const int NUM_ROWS = 6;
	    public const int NUM_COLS = 7;

	    public C4Board()
	    {
	        resetBoard();
    	}

        public C4Board(C4Board board)
        {
            setupBoard(board);
        }

        public C4Board(AbstractBoard board)
        {
            if (board is C4Board)
            {
                C4Board c4_board = board as C4Board;

                // Call C4Board ctor 
                setupBoard(c4_board);
            }
        }

        private void setupBoard(C4Board board)
	    {
	        // Copy squares
	        for(int row_idx = 0; row_idx < NUM_ROWS; ++row_idx)
	        {
	            for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	            {
		            squares[row_idx, col_idx] = board.squares[row_idx, col_idx];
		        }
	        }

	        blackToMove = board.blackToMove;

	        // Copy numPieceInCol
	        for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	        {
    		    numPiecesInCol[col_idx] = board.numPiecesInCol[col_idx];
	        }	
	    }

        public override int rateBoard()
	    {
	        int whiteO_score = rateBoard(PieceType.WHITE);
	        int blackX_score = rateBoard(PieceType.BLACK);

	        int result = whiteO_score - blackX_score;
	        return result;
	    }

	    //
	    // Used in testing to set up a specific board
	    //
	    public void setTestPosn()
	    {
	        var move = new C4Board.C4_Move();
	        move.col_idx = 3; makeMove(move);
		        move.col_idx = 5; makeMove(move);
	        move.col_idx = 4; makeMove(move);
		        move.col_idx = 4; makeMove(move);
	        move.col_idx = 2; makeMove(move);

	        // -------
            // ----O--
	        // --XXXO-
	    }

	    /*
	     * Work out how much credit to give the given colour in the 
         * current position
	     */
	    public int rateBoard(PieceType colour)
	    {
	        const int THREE_IN_LINE_WITH_GAP_SCORE = 100;
	        int result = 0;

	        // Try moving in each of the empty squares and see if this
	        // gives a win:
	        // TO DO: Look for a move that gives us 3+ a gap in a block of 4.

	        // Copy squares
	        for(int row_idx = 0; row_idx < NUM_ROWS; ++row_idx)
	        {
	            for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	            {
    		        if(squares[row_idx, col_idx] == PieceType.EMPTY)
	    	        {
		        	    System.Console.WriteLine($"Considering row {row_idx} col {col_idx}");
        			    var new_board = new C4Board(this);

		        	    if(colour == PieceType.WHITE)
			                new_board.blackToMove = true;
        			    else
		        	        new_board.blackToMove = false;

        			    MoveResult res = new_board.placePiece(row_idx, col_idx);

		        	    if(res.was_winning_move)
        			    {
		        	        // Give more credit to a lower winning move than a higher up one
        			        result += THREE_IN_LINE_WITH_GAP_SCORE;
		        	        result += NUM_COLS - col_idx;
			                System.Console.WriteLine("3 in line with gap found");
        			    }
		            }	
    		    } // end for col
	        } // end for row
	        return result;
	    }

	    private void resetBoard()
	    {
	        for(int row_idx = 0; row_idx < NUM_ROWS; ++row_idx)
	        {
	            for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	            {
	    	        squares[row_idx, col_idx] = PieceType.EMPTY;	    
		        }
	        }	

	        blackToMove = false;
	        numPiecesInCol = new int[NUM_COLS];

	        for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	        {
    		    numPiecesInCol[col_idx] = 0;;	    
            }

	    }

	    public bool canMoveInColumn(int col_idx)
	    {
	        int num_moves_already = numPiecesInCol[col_idx];

	        if(num_moves_already < NUM_ROWS)
	    	    return true;
	        else
		        return false;
	    }

	    public override List<CommonMove> validMoves()
	    {
	        List<CommonMove> result = new List<CommonMove>();

	        for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	        {
    		    if(canMoveInColumn(col_idx) )
	    	    {
		            C4_Move move = new C4_Move();
		            move.col_idx = col_idx;
    		        result.Add(move);
	    	    }
	        }

	        return result;
	    }

	    private bool isMatchOver(int last_move_row, int last_move_col)
	    {
	        bool result = false;
	        /*PieceType this_player = PieceType.WHITE;
	
	        if(blackToMove)
		    this_player = PieceType.BLACK;*/

	        var utils = new C4_BoardUtils(squares);
	        result = utils.isMatchOver(last_move_row : last_move_row,
		  		                       last_move_col : last_move_col,	
				                       is_black_to_move : blackToMove);
	        return result;
	    }

	    //
	    // Returns true if move was a winning one
	    //
	    public override MoveResult makeMove(CommonMove move)
	    {
	        MoveResult result;
	        if( !(move is C4_Move) )
	        {
    		    result = new MoveResult(false, false);
	    	    return result;
	        }

	        C4_Move c4_move = move as C4_Move;

	        int col_idx = c4_move.col_idx;
	
	        // TO DO: Check that this is a valid move? This will take extra 
	        // time though

	        int num_moves_already = numPiecesInCol[col_idx];
	        ++numPiecesInCol[col_idx];

	        if(num_moves_already >= NUM_ROWS)
	        {
		        result = new MoveResult(false, false);
		        return result;
	        }
	    
	        return placePiece(num_moves_already, col_idx);
	    }


	    // TO DO: Fill in
	    private MoveResult placePiece(int row_idx, int col_idx)
	    {
	        MoveResult result;

	        PieceType curr_piece = PieceType.WHITE;

	        if(blackToMove)
		    curr_piece = PieceType.BLACK;

	        squares[row_idx, col_idx] = curr_piece;

	        bool over = isMatchOver(row_idx, col_idx);

	        blackToMove = !blackToMove;

	        result = new MoveResult(winning_move : over,
				        invalid_move : false);

	        return result;
	    }

	    public override void makeBestMove()
	    {
	        var move_finder = new BestMoveFinder(this);

	        AbstractBoard.CommonMove best_move = move_finder.findBestMove();
	        makeMove(best_move);
	    }

	    public override string ToString()
	    {
	        StringBuilder str_builder = new StringBuilder();

	        for(int row_idx = NUM_ROWS - 1; row_idx >= 0; --row_idx)
	        {
	            for(int col_idx = 0; col_idx < NUM_COLS; ++col_idx)
	            {
    		        PieceType curr_piece = squares[row_idx, col_idx];

	    	        if(curr_piece == PieceType.EMPTY)
		        	    str_builder.Append("-");
    		        else if(curr_piece == PieceType.WHITE)
	        		    str_builder.Append("W");
		            else 
        			    str_builder.Append("B");
		        } // end col
		        str_builder.Append(".\n");
	        } // end row
	        return str_builder.ToString();
	    }

    	// 0, 0 is bottom-left
    	public PieceType[,] squares = new PieceType[NUM_ROWS, NUM_COLS];

    	private int[] numPiecesInCol = new int[NUM_COLS];
    }

}