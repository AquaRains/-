using System;
using System.Collections.Generic;
using System.Linq;


namespace sudoku.Resolution
{
    //스도쿠 풀이 알고리즘

    /*
     * 1. 사용가능한 숫자 목록을 한가득 채운다음 안 되는 부분 소거
     * 2. 사용가능한 숫자가 하나일 경우 그 숫자 채워넣기.
     * 3. 다른 전략 몇개 더 채워 넣기

    */
    static class Resolve
    {
        /// <summary>
        /// board의 해를 구합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        static bool resolveBoard(ref Board board)
        {
            return resolveBoard(board, out board);
        }
        static bool resolveBoard(Board board, out Board outBoard)
        {
            outBoard = new Board();
            return true;
        }
    }
}
