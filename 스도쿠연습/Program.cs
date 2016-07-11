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
        int[] temp81Q = new int[81];
        int[] temp9Q = new int[81];
        int[,,] board = new int[3, 3, 3]; 

        public sudoku() // 생성자
        {
            initializeBoard();
            generate81Q();
        }
        public void initializeBoard()
        {
           for(int i=0;i<3; i++)
            {
                for(int j =0;j<3;j++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        board[i, j, k] = 0;
                    }
                }
            }
        }

        
        private int[] generateLine()  //라인 한 줄(혹은 한 덩어리) 생성
        {
            Random rd = new Random(DateTime.Now.Millisecond);

            int[] result = new int[9];

            for(int i = 0; i < 9; i++)
            {
                result[i] = i;
            }

            for(int i = 0; i<9; i++)
            {
                int a = rd.Next(9);  // 0~9 난수발생해서 아무렇게나 자리를 바꾼다
                int b = rd.Next(9);

                int temp = result[a];
                result[a] = result[b];
                result[b] = temp;
            }
            return result;
        }
        private void generate81Q() // 임시 9 덩어리 한 묶음 생성
        {
            for(int i = 0; i < 9; i++)
            {
                Array.Copy(generateLine(), temp81Q, i * 9);
            }
        }

        private static bool boxAvailableCheck(int boxnum, int innerRownum, int innerColnum) //네모 '부분' 체크
        {
            return false;
        }
        private static bool horizontalAvailableCheck(int boxnum,int innerRownum, int innerColnum) // 가로 '부분' 체크
        {
            return false;
        }
        private static bool verticalAvailableCheck(int boxnum,int innerRownum, int innerColnum) // 세로 '부분' 체크
        {
            return false;
        }

        private static bool AvailableCheck(int boxnum, int innerRownum, int innerColnum) //한무데기 부분 체크
        {
            return boxAvailableCheck(boxnum, innerRownum, innerColnum) && horizontalAvailableCheck(boxnum, innerRownum, innerColnum) && verticalAvailableCheck(boxnum, innerRownum, innerColnum);
        }


        public bool BoardCheck() // 판 전체 체크
        {
            bool result = true;
            int count = 0;
            while (result && count >= 81)
            {
                result = result && AvailableCheck(count / 9, count / 3, count % 3);
                count++;
            }
            return result;
        }


    }
    
}
