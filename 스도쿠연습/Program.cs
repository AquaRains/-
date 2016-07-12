using System;

namespace 스도쿠연습
{
    class Program
    {
        static void Main(string[] args)
        {

           sudoku Sudoku = new sudoku();

            int[,,] temp = Sudoku.GetBoard();
            


           
            Console.WriteLine();
            Console.ReadLine();
            
        }
        
    }


    
}
