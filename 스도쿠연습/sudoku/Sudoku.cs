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
    class Sudoku
    {
        public Board Board { get; set; }
        public string Name { get; set; }
        public ResolveResult ResolveResult { get; private set; }

        public Sudoku()
        {
            Board = new Board();
            Name = DateTime.Now.ToLongTimeString();
        }


    }
}
