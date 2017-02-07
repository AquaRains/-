namespace sudoku.Resolution
{
    //스도쿠 풀이 알고리즘

    /*
     * 1. 사용가능한 숫자 목록을 한가득 채운다음 안 되는 부분 소거
     * 2. 사용가능한 숫자가 하나일 경우 그 숫자 채워넣기.
     * 3. 다른 전략 몇개 더 채워 넣기

    */
    /// <summary>
    /// 스도쿠 판 해를 구하는 기능을 모아놓은 클래스입니다.
    /// </summary>
    public class Resolve
    {
        private Board board;
        public Board Board { get => board; }
        public Board answer
        {
            get
            {
                Board answerboard;
                resolveBoard(board, out answerboard);
                return answerboard;
            }
        }


        public Resolve(Board board) =>  this.board = board;
        public Resolve()
        {
            this.board = new Board();
            Generator.BoardGenarator.genarateboard(this.board);
        }

        /// <summary>
        /// board의 해를 구합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static bool resolveBoard(ref Board board)
        {
            return resolveBoard(board, out board);
        }
        /// <summary>
        /// board의 해를 구합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="outBoard"></param>
        /// <returns></returns>
        public static bool resolveBoard(Board board, out Board outBoard)
        {
            Board curruntboard = new Board();
            
          
            outBoard = new Board();
            return true;
        }

        /// <summary>
        /// board의 point에 value를 입력할 수 없을때 사용합니다.
        /// board의 point의 available number중 value 값을 삭제합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        private static void eliminate(Board board, Point point, int value)
        {
            //값을변경하고
            //유효성검사를 한다
            //성공하면 그대로 리턴
            //실패하면 롤백
        }
        /// <summary>
        /// board의 point에 value 값을 입력하고, 연관된 모든 값의 조건을 갱신합니다.
        /// 정확히는, 연관된 모든 칸의 available number에서 value를 eliminate합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        private static void assign(Board board, Point point, int value)
        {
            //대상 point 각각 eliminate를 실행
            //이 중에 하나라도 실패하면, 롤백.
      
            //성공하면 그대로
        }
    }
}
