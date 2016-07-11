using System;

namespace 스도쿠연습
{
    class Program
    {
        static void Main(string[] args)
        {

           sudoku Sudoku = new sudoku();
            
            int[] temp = Sudoku.board;

          
            for (int i = 0; i < temp.Length; i++)
            {
                if (i % 9 == 0)
                {
                    Console.Write("\n\n");
                }
                Console.Write(temp[i] + "\t");
               
            }
    
            
            Console.WriteLine();
            Console.ReadLine();
        }
        
    }

    public class sudoku
    {
        int tempQ;
        int[,,] board = new int[3, 3, 3];

    }
    
}
