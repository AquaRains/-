using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Resolution
{
    interface ICheckable
    {
        bool availableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum);
        bool availableCheck(int[] nums);
    }
    
}
