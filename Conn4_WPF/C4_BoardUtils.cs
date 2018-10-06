
using System;
using System.Collections.Generic;
using System.Text;

namespace Conn4_WPF
{
    class C4_BoardUtils
    {
    	private C4Board.PieceType[,] squares;

    	// Num squares needed to win in row in addition
    	// to own last move:
    	private const int NUM_EXTRA_SQS_NEEDED = 3;
	
    	public C4_BoardUtils(C4Board.PieceType[,] squares)
    	{
	         // Shallow copy
	      this.squares = squares;
	    }

    	//
    	// Returns a count of how many consecutive counters there are downwards
    	// the same as this piece's colour
    	//
    	public int numSameCountersDown(int last_move_row, int last_move_col, C4Board.PieceType this_player)
    	{
    	    int num_in_line = 0;
	    
	        for(int row_idx = last_move_row - 1; row_idx >= 0; --row_idx)
	        {
        		if(squares[row_idx, last_move_col] == this_player)
	        	{
		            num_in_line++;
		        }
        		else
		        {
		            break;
        		}
    	    }
            return num_in_line;
	    }

	    //
	    // Returns a count of how many consecutive counters there are leftwards
	    // the same as this piece's colour
	    //
	    public int numSameCountersLeft(int last_move_row, int last_move_col, C4Board.PieceType this_player)
	    {
	        int num_in_line = 0;
	    
	        for(int col_idx = last_move_col - 1; col_idx >= 0; --col_idx)
	        {
    		    if(squares[last_move_row, col_idx] == this_player)
	    	    {
		            num_in_line++;
    		    }
	    	    else
	        	{
	    	        break;
	    	    }
	        }
            return num_in_line;
	    }

    	//
    	// Returns a count of how many consecutive counters there are rightwards
    	// the same as this piece's colour
    	//
    	public int numSameCountersRight(int last_move_row, int last_move_col, C4Board.PieceType this_player)
    	{
    	    int num_in_line = 0;
	        
	        for(int col_idx = last_move_col + 1; col_idx < C4Board.NUM_COLS; ++col_idx)
	        {
		        if(squares[last_move_row, col_idx] == this_player)
		        {
		            num_in_line++;
		        }
        		else
		        {
		            break;
        		}
    	    }
            return num_in_line;
	    }

	    public int numSameCountersBottomLeft(int last_move_row, int last_move_col, C4Board.PieceType this_player)
	    {
	        int num_in_line = 0;

	        int col_idx = last_move_col -1;
	        int row_idx = last_move_row -1;
	    
	        for(; (col_idx >= 0) && (row_idx >= 0); --col_idx, --row_idx)
	        {
    		    if(squares[row_idx, last_move_col] == this_player)
	    	    {
		            num_in_line++;
    		    }
	    	    else
		        {
		            break;
    		    }
	        }
            return num_in_line;
	    }

	    public int numSameCountersTopRight(int last_move_row, int last_move_col, C4Board.PieceType this_player)
	    {
	        int num_in_line = 0;

	        int col_idx = last_move_col + 1;
	        int row_idx = last_move_row + 1;
	    
	        for(; (col_idx < C4Board.NUM_COLS) && (row_idx < C4Board.NUM_ROWS); ++col_idx, ++row_idx)
	        {
	    	    if(squares[row_idx, last_move_col] == this_player)
	    	    {
	    	        num_in_line++;
    		    }
	    	    else
		        {
		            break;
    		    }
	        }
            return num_in_line;
	    }

	    public int numSameCountersBottomRight(int last_move_row, int last_move_col, C4Board.PieceType this_player)
	    {
	        int num_in_line = 0;

	        int col_idx = last_move_col + 1;
	        int row_idx = last_move_row - 1;
	    
	        for(; (col_idx < C4Board.NUM_COLS) && (row_idx >= 0); ++col_idx, --row_idx)
	        {
	    	    if(squares[row_idx, last_move_col] == this_player)
		        {
		            num_in_line++;
	    	    }
		        else
		        {
		            break;
    		    }
	        }
            return num_in_line;
	    }

	    public int numSameCountersTopLeft(int last_move_row, int last_move_col, C4Board.PieceType this_player)
	    {
	        int num_in_line = 0;

	        int col_idx = last_move_col - 1;
	        int row_idx = last_move_row + 1;
	    
	        for(; (row_idx < C4Board.NUM_ROWS) && (col_idx >= 0); --col_idx, ++row_idx)
	        {
	    	    if(squares[row_idx, last_move_col] == this_player)
		        {
		            num_in_line++;
    		    }
	    	    else
		        {
		            break;
    		    }
	        }
            return num_in_line;
	    }

	    //
	    // Looks to see if there are 4 in a row directly down 
	    //
	    private bool is4InARowDownwards(int last_move_row, int last_move_col, C4Board.PieceType this_player)
 	    {
	        bool result = false;
	        int num_in_line = numSameCountersDown(last_move_col : last_move_col, 
						      last_move_row : last_move_row,
						      this_player   : this_player);

	        if(num_in_line >= NUM_EXTRA_SQS_NEEDED)
	        {
                // Match over
                result = true;
	        }
	        return result;
	    }

	    //
	    // Looks to see if there are 4 in a row left and right
	    //
	    private bool is4InARowLeftAndRight(int last_move_row, int last_move_col, C4Board.PieceType this_player)
 	    {
	        bool result = false;
	        int num_in_line = numSameCountersLeft(last_move_col : last_move_col, 
			                    			      last_move_row : last_move_row,
						                          this_player   : this_player);

	        num_in_line += numSameCountersRight(last_move_col : last_move_col, 
						                        last_move_row : last_move_row,
						                        this_player   : this_player);

	        if(num_in_line >= NUM_EXTRA_SQS_NEEDED)
	        {
                // Match over
	    	    result = true;
	        }
	        return result;
	    }

	    //
	    // Looks to see if there are 4 in a row in a line from bottom-left to top-right
	    //
	    private bool is4InARowBL_TR(int last_move_row, int last_move_col, C4Board.PieceType this_player)
 	    {
	        bool result = false;
	        int num_in_line = numSameCountersBottomLeft(last_move_col : last_move_col, 
		                    			  	            last_move_row : last_move_row,
						                                this_player   : this_player);

	        num_in_line += numSameCountersTopRight(last_move_col : last_move_col, 
                    						       last_move_row : last_move_row,
					                    	       this_player   : this_player);

	        if(num_in_line >= NUM_EXTRA_SQS_NEEDED)
	        {
                // Match over
                result = true;
	        }
	        return result;
	    }

	    //
	    // Looks to see if there are 4 in a row in a line from bottom-right to top-left
	    //
	    private bool is4InARowBR_TL(int last_move_row, int last_move_col, C4Board.PieceType this_player)
 	    {
	        bool result = false;
	        int num_in_line = numSameCountersBottomRight(last_move_col : last_move_col, 
		                    			  	             last_move_row : last_move_row,
						                                 this_player   : this_player);

	        num_in_line += numSameCountersTopLeft(last_move_col : last_move_col, 
                    						      last_move_row : last_move_row,
					                    	      this_player   : this_player);

	        if(num_in_line >= NUM_EXTRA_SQS_NEEDED)
	        {
                // Match over
                result = true;
	        }
	        return result;
	    }

	    public bool isMatchOver(int last_move_row, int last_move_col, bool is_black_to_move)
	    {
	        bool result = false;
	        C4Board.PieceType this_player = C4Board.PieceType.WHITE;

	        if(is_black_to_move)
		        this_player = C4Board.PieceType.BLACK;
	
		    if(is4InARowDownwards(last_move_row: last_move_row, last_move_col: last_move_col, this_player: this_player) )
		    {
		        result = true;
		    }

		    if(!result && is4InARowLeftAndRight(last_move_row: last_move_row, last_move_col: last_move_col, this_player: this_player) )
		    {
		        result = true;
		    }

		    if(!result && is4InARowBL_TR(last_move_row: last_move_row, last_move_col: last_move_col, this_player: this_player) )
		    {
		        result = true;
		    }

		    if(!result && is4InARowBR_TL(last_move_row: last_move_row, last_move_col: last_move_col, this_player: this_player) )
		    {
		        result = true;
		    }

            return result;    
        }

    }

}