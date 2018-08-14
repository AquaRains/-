using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.Generator;
using Sudoku.Resolution;

namespace Sudoku
{
    /// <summary>스도쿠 전체 클래스입니다. 하나의 게임을 의미합니다.</summary>
    public class Sudoku
    {
        /// <summary>이 게임의 저장된 보드 상태를 가져옵니다. </summary>
        public Board Board { get; set; }
        /// <summary> 게임의 이름 </summary>
        public string Name { get; set; }
        /// <summary> 게임 풀이 결과</summary>
        public ResolveResult ResolveResult { get; set; } = ResolveResult.UnKnown;

        public Sudoku()
        {
            Board = new Board();
            Name = DateTime.Now.ToLongTimeString();
        }

        public void Genarate()
        {
            Board = BoardGenarator.genarateboard(Board);
        }
        
        public bool BoardTest()
        {
            var temp = (Board)Board.Clone();
            return Resolve.resolveBoard(ref temp);
        }
    }
}
