using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;
using Sudoku.Base;

namespace Sudoku.Base
{
    public abstract class SudokuBase
    {
        
    }


    public delegate void OnValueChangedEventHandler(object o, OnValueChangedEventArgs e);

    public class OnValueChangedEventArgs : EventArgs
    {
        SudokuBase Parent;
        object value;

        public OnValueChangedEventArgs(SudokuBase parent, object value = null) : base()
        {
            this.Parent = parent;
            this.value = null;
            switch  (value)
            {
                case Cell c:
                    {
                        this.value = c.value;
                        break;
                    }
                case Longline l:
                    {
                        this.value = l.LineValues;
                        break;
                    }
                case Box b:
                    {
                        this.value = b.box;
                        break;
                    }
                default:
                    {
                        break;
                    }
                
            }

        }
    }
}
