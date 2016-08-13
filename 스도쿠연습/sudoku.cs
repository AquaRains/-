using System;
using System.Linq;

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

        protected int[] temp9Q = new int[9];           //그 칸에 가능한 숫자
        protected int[,][,][] tempexQ = new int[3, 3][,][];  // 그 칸에 가능한 숫자 목록
        protected int[,][,] Board = new int[3, 3][,];   // 게임 판
        public int[,][,] board
        {
            get
            {
                return Board;
            }
            protected set
            {
                Board = value;
            }
        }
        public readonly int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        protected int[,][,] _answer = new int[3, 3][,]; //답안지, board값을 복사해 놓을 예정이지만, 주로 힌트에나 쓰고 답 체크는 밑에 check어쩌구 하는 놈들로 쓸 예정.

        /// <summary>
        /// 게임 판의 답안을 저장해두었다가 출력해줍니다.
        /// </summary>
        public int[,][,] answer { get { return _answer; } protected set { _answer = value; ; } }
        

        /// <summary>
        /// 클래스 생성자 : 보드 초기화와 임시 배치용 배열 생성
        /// </summary>
        public sudoku() // 생성자
        {
            initializeBoard();
            
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
                    Board[i, j] = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } } ;
                  
                }
            }
        }


        /// <summary>
        /// 1차원 배열을 3x3배열로
        /// </summary>
        /// <param name="line">size 9의 1차원 배열</param>
        /// <returns></returns>
        protected int[,] LineTobox(int[] line)
        {
            int[,] box = new int[3, 3];
            int p = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    box[i, j] = line[p];
                    p++;
                }
            }

            return box;
        }

        /// <summary>
        /// 3x3을 1차원으로
        /// </summary>
        /// <param name="box">3x3의 2차원 배열</param>
        /// <returns></returns>
        protected int[] BoxtoLine(int[,] box)
        {
            int[] line = new int[9];

            for (int i = 0; i < 9; i++)
            {
                line[i] = box[i % 3, i / 3];
            }

            return line;
        }

        /// <summary>
        /// 특정 좌표의 값 불러옴
        /// </summary>
        /// <param name="boxRownum"></param>
        /// <param name="boxColnum"></param>
        /// <param name="innerRownum"></param>
        /// <param name="innerColnum"></param>
        /// <returns></returns>
        public int point(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            return Board[boxRownum, boxColnum][innerRownum, innerColnum];
        }
 


        /// <summary>
        /// 부분 네모 체크 메서드
        /// </summary>
        /// <param name="boxRownum">큰 사각형 행 번호</param>
        /// <param name="boxColnum">큰 사각형 열 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        /// 

        #region 조건체크파트
        protected bool boxAvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum) //네모 '부분' 체크
        {
            int a = point(boxRownum, boxColnum, innerRownum, innerColnum);
            return Board[boxRownum, boxColnum][innerRownum, innerColnum] == 0 || Array.FindAll(BoxtoLine(Board[boxRownum, boxColnum]), x => x == a).Count() <= 1;
        }
        protected bool boxAvailableCheck(int[] a)
        {
            return boxAvailableCheck(a[0], a[1], a[2], a[3]);
        } // 오버로딩
        public int[,] Getbox(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            return Board[boxRownum, boxColnum];
        }

        /// <summary>
        /// 부분 가로 체크 메서드
        /// </summary>
        /// <param name="boxRownum">큰 사각형 행 번호</param>
        /// <param name="boxColnum">큰 사각형 열 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        protected bool horizontalAvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum) // 가로 '부분' 체크
        {
            int a = point(boxRownum, boxColnum, innerRownum, innerColnum);
            int[] line = GethorizontalList(boxRownum, boxColnum, innerRownum, innerColnum);
            return Board[boxRownum, boxColnum][innerRownum, innerColnum] == 0 || Array.FindAll(line, x => x == a).Count() <= 1;
        }
        protected bool horizontalAvailableCheck(int[] a)
        {
            return horizontalAvailableCheck(a[0], a[1], a[2], a[3]);
        }  //오버로딩
        /// <summary>
        /// 가로 쭈우우욱 값 가져옴
        /// </summary>
        /// <param name="boxRownum">큰 사각형 행 번호</param>
        /// <param name="boxColnum">큰 사각형 열 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns></returns>
        public int[] GethorizontalList(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            int a = point(boxRownum, boxColnum,innerRownum, innerColnum);
            int[] line = new int[9];
            int p = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    line[p] = Board[boxRownum, i][innerRownum, j];
                    p++;
                }
            }
            return line;
        }




        /// <summary>
        /// 부분 세로 체크 메서드
        /// </summary>
        /// <param name="boxRownum">큰 사각형 행 번호</param>
        /// <param name="boxColnum">큰 사각형 열 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        protected bool verticalAvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            int a = point(boxRownum, boxColnum, innerRownum, innerColnum);
            int[] line = GetverticalList(boxRownum, boxColnum, innerRownum, innerColnum);
            return Board[boxRownum, boxColnum][innerRownum, innerColnum] == 0 || Array.FindAll(line, x => x == a).Count() <= 1;

        }
        protected bool verticalAvailableCheck(int[] a)
        {
            return verticalAvailableCheck(a[0], a[1], a[2], a[3]);
        }  //오버로딩
        public int[] GetverticalList(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            int a = Board[boxRownum, boxColnum][innerRownum, innerColnum];
            int[] line = new int[9];
            int p = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    line[p] = Board[i, boxColnum][j, innerColnum];
                    p++;
                }
            }
            return line;
        }
        /// <summary>
        /// 가로, 세로 , 네모를 한꺼번에 체크
        /// </summary>
        /// <param name="boxRownum">큰 사각형 행 번호</param>
        /// <param name="boxColnum">큰 사각형 열 번호</param>
        /// <param name="innerRownum">사각형 내부의 행 번호</param>
        /// <param name="innerColnum">사각형 내부의 열 번호</param>
        /// <returns>규칙 체크 결과를 출력합니다</returns>
        protected bool AvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            int[] a = { boxRownum, boxColnum, innerRownum, innerColnum };
            return boxAvailableCheck(a) && horizontalAvailableCheck(a) && verticalAvailableCheck(a);
        }
/// <summary>
/// 판 전체를 체크
/// </summary>
/// <returns></returns>
public bool BoardCheck()
{
    bool result = true;

    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { for (int k = 0; k < 3; k++) { for (int l = 0; l < 3; l++) { result &= AvailableCheck(i, j, k, l); } } } }

    return result;
}
#endregion

#region Get/Set Board
/// <summary>
/// 게임 판(3x3x3x3 배열)을 불러옵니다. 알아서 쓰셈.
/// </summary>
/// <returns></returns>

#endregion
}

    /// <summary>
    /// 기본 게임 판 생성에 관여하는 class
    /// </summary>
    public class 스도쿠board : sudoku
    {
        public 스도쿠board()
        {
            initializeBoard();
            makeBoard();
        }

        #region makeboard 관련
        /// <summary>
        /// 게임 판을 만듭니다...만 시바 이거 만드는게 젤 X같다고 진짜.
        /// </summary>
        public void makeBoard()
        {
            int[] line = generateLine();
            int[,] firstbox = LineTobox(line);
            Board[0, 0] = firstbox;
            int[] line_2 = lineLeftSwitch(line);
            int[,] secondbox = LineTobox(line_2);
            Board[1, 0] = secondbox;
            int[] line_3 = lineLeftSwitch(line_2);
            int[,] thirdbox = LineTobox(line_3);
            Board[2, 0] = thirdbox;
            for (int i = 1; i < 3; i++)
            {
                Board[0, i] = boxswap(Board[0, i - 1]);
                Board[1, i] = boxswap(Board[1, i - 1]);
                Board[2, i] = boxswap(Board[2, i - 1]);
            }
            boxrowshuffle(Board);
            boxcolshuffle(Board);

            answer = (int[,][,])Board.Clone();
        }
        /// <summary>
        /// 길이 9의 배열 요소를 앞으로 한칸 밉니다
        /// </summary>
        /// <param name="input">크기 9의 int 배열</param>
        /// <returns></returns>
        private int[] lineLeftSwitch(int[] input)
        {
            int[] line_temp = new int[8];
            Array.Copy(input, 1, line_temp, 0, 8);
            int[] line = new int[9];
            line_temp.CopyTo(line, 0);
            line[8] = input[0];

            return line;
        }
        /// <summary>
        /// 상자 내용물을 위로 한줄씩 땡김
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int[,] boxswap(int[,] input)
        {
            int[,] result = new int[3, 3];

            int[] line_1 = new int[3];
            int[] line_2 = new int[3];
            int[] line_3 = new int[3];

            for (int i = 0; i < 3; i++)
            {
                line_1[i] = input[0, i];
                line_2[i] = input[1, i];
                line_3[i] = input[2, i];
            }
            for (int i = 0; i < 3; i++)
            {
                result[0, i] = line_2[i];
                result[1, i] = line_3[i];
                result[2, i] = line_1[i];
            }
            return result;
        }
        private T[,] boxrowshuffle<T>(T[,] input)
        {
            T[,] result = new T[3, 3];
            T[][] lines = new T[3][];
            Random r = new Random(System.DateTime.Now.Millisecond);

            lines[0] = new T[3] { input[0, 0], input[0, 1], input[0, 2] };
            lines[1] = new T[3] { input[1, 0], input[1, 1], input[1, 2] };
            lines[2] = new T[3] { input[2, 0], input[1, 1], input[2, 2] };


            T[] templine = new T[3];
            for (int i = 0; i < 3; i++)
            {
                int a = r.Next(3);
                int b = r.Next(3);
                templine = lines[a];
                lines[a] = lines[b];
                lines[b] = templine;
            }

            for (int i = 0; i < 3; i++)
            {
                result[0, i] = lines[0][i];
                result[1, i] = lines[1][i];
                result[2, i] = lines[2][i];
            }

            return result;
        }
        private T[,] boxcolshuffle<T>(T[,] input)
        {
            T[,] result = new T[3, 3];
            T[][] lines = new T[3][];
            Random r = new Random(System.DateTime.Now.Millisecond);

            lines[0] = new T[3] { input[0, 0], input[1, 0], input[2, 0] };
            lines[1] = new T[3] { input[0, 1], input[1, 1], input[2, 1] };
            lines[2] = new T[3] { input[0, 2], input[1, 2], input[2, 2] };


            T[] templine = new T[3];
            for (int i = 0; i < 3; i++)
            {
                int a = r.Next(3);
                int b = r.Next(3);
                templine = lines[a];
                lines[a] = lines[b];
                lines[b] = templine;
            }

            for (int i = 0; i < 3; i++)
            {
                result[0, i] = lines[i][0];
                result[1, i] = lines[i][1];
                result[2, i] = lines[i][2];
            }

            return result;
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
                result[i] = i + 1;
            }

            for (int i = 0; i < 18; i++)
            {
                int a = rd.Next(9);  // 0~9 난수발생해서 아무렇게나 자리를 바꾼다
                int b = rd.Next(9);



                int temp = result[a];
                result[a] = result[b];
                result[b] = temp;
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// 해 구하는 상속받은 클래스, 생성자는 기존 스도쿠 인스턴스 이용.
    /// </summary>
    public class 스도쿠resolve : sudoku
    {
        //스도쿠 풀이 알고리즘

        /* 1단계 : 바로 입력 가능한 유일 솔루션 채우기
        * 2단계 : 1/2, 1/3 선택지 숫자 메모하기
        *3단계 : 숫자 블럭 만들기
        * 4단계 : 시나리오 테스트로 시행착오법 적용하기
        */
        private int phasecount = 0;


        public 스도쿠resolve(sudoku 스도쿠)
        {
            this.board = 스도쿠.board;
        }
        public void resolveBoard()
        {
            while (phasecount < 4)
            {
                switch (phasecount)
                {
                    case 0:
                        phase1();
                        break;
                    case 1:
                        phase2();
                        break;
                    case 2:
                        phase3();
                        break;
                    case 3:
                        phase4();
                        break;
                    default:
                        break;
                } 
            }

        }
        /// <summary>
        ///  1단계 : 바로 입력 가능한 유일 솔루션 채우기
        /// </summary>
        private void phase1()
        {
            for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { for (int k = 0; k < 3; k++) { for (int l = 0; l < 3; l++) {
                            int a = point(i, j, k, l);
                            int[] vl = GetverticalList(i, j, k, l);
                            int[] hl = GethorizontalList(i, j, k, l);
                            int[,] box = Getbox(i, j, k, l);


                        } } } }
        }
        private void phase2()
        {

        }
        private void phase3()
        {

        }
        private void phase4()
        {

        }

        public void availablecheck(sudoku 스도쿠)
        {

        }
    }
    public class 스도쿠question : sudoku
    {

        //스도쿠 출제 알고리즘

        /*
        1. 9x9 랜덤 스도쿠 행렬을 생성한다.
        <반복 시작>
        2. 하나의 숫자를 지운다.
        3. 숫자를 지운 후 스도쿠 퍼즐이 풀어지는지 확인한다.
        <스도쿠 퍼즐을 풀 수 있을 경우 2~3을 반복>
        4. 스도쿠 퍼즐이 풀어지지 않을 경우(유일한 답이 나오지 않는 경우를 의미),
        가장 마지막에 지운 숫자를 되돌리면 스도쿠 퍼즐이 완성된다. 
        */

        public void generateQuestion()
        {

        }
    }
}
   
