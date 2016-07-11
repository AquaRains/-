using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스도쿠연습
{
    public class sudoku
    {
        private int[] temp81Q = new int[81];
        private int[] temp9Q = new int[81];
        private int[,,] board = new int[3, 3, 3];


        /// <summary>
        /// 클래스 생성자 : 보드 초기화와 임시 배치용 배열 생성
        /// </summary>
        public sudoku() // 생성자
        {
            initializeBoard();
            generate81Q();
        }

        /// <summary>
        /// 판 초기화 : 모든 요소를 0으로 가득 채움.
        /// </summary>
        public void initializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
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

            for (int i = 0; i < 9; i++)
            {
                result[i] = i;
            }

            for (int i = 0; i < 9; i++)
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
            for (int i = 0; i < 9; i++)
            {
                Array.Copy(generateLine(), 0, temp81Q, i * 9, 9);
            }
        }
        /// <summary>
        /// 부분 네모 체크 메서드
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private static bool boxAvailableCheck(int boxnum, int innerRownum, int innerColnum) //네모 '부분' 체크
        {
            return false;
        }
        /// <summary>
        /// 부분 가로 체크 메서드
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private static bool horizontalAvailableCheck(int boxnum, int innerRownum, int innerColnum) // 가로 '부분' 체크
        {
            return false;
        }

        /// <summary>
        /// 부분 세로 체크 메서드
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private static bool verticalAvailableCheck(int boxnum, int innerRownum, int innerColnum) // 세로 '부분' 체크
        {
            return false;
        }
        /// <summary>
        /// 가로, 세로 , 네모를 한꺼번에 체크
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private static bool AvailableCheck(int boxnum, int innerRownum, int innerColnum)
        {
            return boxAvailableCheck(boxnum, innerRownum, innerColnum) && horizontalAvailableCheck(boxnum, innerRownum, innerColnum) && verticalAvailableCheck(boxnum, innerRownum, innerColnum);
        }

        /// <summary>
        /// 판 전체를 체크
        /// </summary>
        /// <returns></returns>
        public bool BoardCheck() 
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

        /// <summary>
        /// 게임 판을 불러옵니다. 알아서 쓰셈.
        /// </summary>
        /// <returns></returns>
        public int[,,] GetBoard()
        {
            return board;


        }
        /// <summary>
        /// 게임 판 입력용 메서드. 랜덤으로 생성된 문제가 아닌 기존 문제를 입력할때 씁니다.
        /// </summary>
        /// <param name="array">3x3x3배열만 가능합니다</param>
        public void SetBoard(int[,,] array)
        {
            


        }
    }
}
