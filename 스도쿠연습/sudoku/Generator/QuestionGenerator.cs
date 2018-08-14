using System;
using sudoku.Resolution;
namespace Sudoku.Generator
{

    public class QuestionGenerator
    {
        Board answer = new Board();
        
        public QuestionGenerator(Board board)
        {
            board = BoardGenarator.genarateboard(board);
            
        }

        private static Board makeQuestion(Board board)
        {
            return board;
        }
    }
}
