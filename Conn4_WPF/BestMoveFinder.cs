
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conn4_WPF
{
    public class BestMoveFinder
    {
        const int BIG_SCORE = 100000;
        const int BLACK_WIN = -10000;
        const int WHITE_WIN = 10000;

        // TO DO: Use abstract board throughout
        public BestMoveFinder(C4Board board)
        {
            this.board = board;
        }

        public AbstractBoard.CommonMove findBestMove()
        {
            int alpha = -BIG_SCORE;
            int beta = BIG_SCORE;
            const int MOVE_DEPTH = 1; // TO DO: increase

            var possible_move = new AbstractBoard.CommonMove();

            if (board.blackToMove)
                findBestBlackMove(possible_move, MOVE_DEPTH, alpha, beta, board);
            else
                findBestWhiteMove(possible_move, MOVE_DEPTH, alpha, beta, board);

            System.Console.WriteLine("Best move: " + possible_move.move_idx);

            var result = new AbstractBoard.CommonMove();
            result.move_idx = possible_move.move_idx;
            return result;
        }

        // Fill in....
        int findBestBlackMove(AbstractBoard.CommonMove col_to_move_in, int remaining_depth, int white_min, int black_max, C4Board board)
        {
            int best_score = BIG_SCORE;
            var best_move = new AbstractBoard.CommonMove();
            var move_status = new AbstractBoard.MoveResult();

            List<AbstractBoard.CommonMove> valid_moves = board.validMoves();

            for (int move_idx = 0; move_idx < valid_moves.Count; ++move_idx)
            {
                // Make each move in turn and see if it would be a winning move
                var curr_move = valid_moves[move_idx];
                var new_board = new C4Board(board);
                move_status = new_board.makeMove(curr_move);

                if (move_status.was_winning_move)
                {
                    col_to_move_in = curr_move;
                    return BLACK_WIN;
                }

                int score = 0;

                if (remaining_depth == 0)
                {
                    score = new_board.rateBoard();
                }
                else
                {
                    score = findBestWhiteMove(col_to_move_in: curr_move,
                                              remaining_depth: remaining_depth - 1,
                                              white_min: white_min,
                                              black_max: black_max,
                                              board: new_board);
                }


                if (score < best_score)
                {
                    // Best move yet
                    best_score = score;
                    best_move = col_to_move_in;

                    if (black_max > best_score)
                        black_max = best_score;
                }

                if (black_max <= white_min)
                {
                    // Pruning - short-circuit rest of processing
                    col_to_move_in = best_move;
                    return best_score;
                }
            }

            col_to_move_in = best_move;
            return best_score;
        }

        // Fill in....
        int findBestWhiteMove(AbstractBoard.CommonMove col_to_move_in, int remaining_depth, int white_min, int black_max, C4Board board)
        {
            int best_score = -BIG_SCORE;  // TO DO??
            var best_move = new AbstractBoard.CommonMove();
            var move_status = new AbstractBoard.MoveResult();

            List<AbstractBoard.CommonMove> valid_moves = board.validMoves();

            for (int move_idx = 0; move_idx < valid_moves.Count; ++move_idx)
            {
                // Make each move in turn and see if it would be a winning move
                var curr_move = valid_moves[move_idx];
                var new_board = new C4Board(board);
                move_status = new_board.makeMove(curr_move);

                if (move_status.was_winning_move)
                {
                    col_to_move_in = curr_move;
                    return WHITE_WIN;
                }

                int score = 0;

                if (remaining_depth == 0)
                {
                    score = new_board.rateBoard();
                }
                else
                {
                    score = findBestBlackMove(col_to_move_in: curr_move,
                                              remaining_depth: remaining_depth - 1,
                                              white_min: white_min,
                                              black_max: black_max,
                                              board: new_board);
                }


                // TO DO:
                if (score > best_score)
                {
                    // Best move yet
                    best_score = score;
                    best_move = col_to_move_in;

                    if (white_min < best_score)
                        white_min = best_score;
                }

                // TO DO:
                if (black_max <= white_min)
                {
                    // Pruning - short-circuit rest of processing
                    col_to_move_in = best_move;
                    return best_score;
                }
            }

            col_to_move_in = best_move;
            return best_score;
        }

        private C4Board board;
    }
}