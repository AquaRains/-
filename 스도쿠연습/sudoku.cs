using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스도쿠연습
{
    /// <summary>
    /// 스도쿠. 만들기 존나 빡세네 진짜 ㅅㅂ 때려치고싶은데 내가 저지른거라
    /// </summary>
    /// <remarks>
    /// 여기서 쓰는 판 배열은 내부사각형(3x3),사각형 내부 행,열로 구성되어 있음.
    /// </remarks>
    public class sudoku
    {
        private int[] temp81Q = new int[81];        //게임판 생성때 사용할 임시 숫자 묶음 ( 1~ 9로 9벌)
        private int[] temp9Q = new int[9];           //그 칸에 가능한 숫자
        private int[,,] board = new int[3, 3, 3];       // 게임 판
        private int[,,][] tempexQ = new int[3, 3, 3][];   // 그 칸에 가능한 숫자 목록


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


        /// <summary>
        /// 한 벌의 숫자 묶음 생성
        /// </summary>
        /// <returns>임의 순서의 1~9의 숫자가 들어간 배열 출력</returns>
        private int[] generateLine()
        {
            Random rd = new Random(DateTime.Now.Millisecond);

            int[] result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = i+1;
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
        /// <summary>
        /// 빈 게임 판에 배치할 임시 9벌의 숫자 묶음 생성
        /// </summary>
        private void generate81Q()
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
        private bool boxAvailableCheck(int boxnum, int innerRownum, int innerColnum) //네모 '부분' 체크
        {
            int x, y, b;
            x = 0;
            y = 0;
            b = boxnum;
            int[] t = new int[9];
            int p = 0;
            for (y = 0; y < 3; y++)
            {
                for (x = 0; x < 3; x++)
                {
                    t[p] = board[b, x, y];
                    p++;
                }
            }
            return Array.FindAll(t, R => R == board[b, innerRownum, innerColnum]).Count() <= 1 || board[boxnum, innerRownum, innerColnum] == 0;
        }
        /// <summary>
        /// 부분 가로 체크 메서드
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private bool horizontalAvailableCheck(int boxnum, int innerRownum, int innerColnum) // 가로 '부분' 체크
        {
            int[] t = new int[9];
            int x = 0;
            int y = innerColnum;
            int p = 0;
            int bb = boxnum - boxnum % 3;
            for (int b=0 ;b < 3 ; b++)
            {
                for (x = 0; x < 3; x++)
                {
                    t[p] = board[b+ bb, x, y];
                    p++;
                } 
            }
            return Array.FindAll(t, R => R == board[boxnum, innerRownum, innerColnum]).Count() <= 1 || board[boxnum, innerRownum, innerColnum] == 0;
        }

        /// <summary>
        /// 부분 세로 체크 메서드
        /// </summary>
        /// <param name="boxnum">사각형 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        private bool verticalAvailableCheck(int boxnum, int innerRownum, int innerColnum)
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
        private bool AvailableCheck(int boxnum, int innerRownum, int innerColnum)
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
        /// 게임 판(3x3x3 배열)을 불러옵니다. 알아서 쓰셈.
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
            board = array;
        }
    }
}
