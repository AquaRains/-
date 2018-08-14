using System;
using System.Collections.Generic;

namespace Sudoku.Base
{


    /// <summary>
    /// 칸 하나에 저장된 값들의 집합입니다.
    /// </summary>
    public class Cell : SudokuBase, IEquatable<Cell>
    {

        /// <summary>
        /// 이곳에 넣을 수 있는 값의 집합
        /// </summary>
        public int[] availableNumList = new int[9];
        private int _value;

        public object Parent { get; private set; }
        public Cell(int value) { this.value = value; }

        public Cell()
        {
            value = 0;
        }

        /// <summary>
        /// Cell 내부에 저장되어있는 숫자를 나타냅니다
        /// </summary>
        public int value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged.Invoke(this, new OnValueChangedEventArgs(this, value));
            }
        }
        /// Cell.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator int(Cell v)
        {
            return v.value;
        }
        /// <summary>
        /// Cell.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator Cell(int v)
        {
            return new Cell(v);
        }

        public event OnValueChangedEventHandler OnValueChanged;

        #region IEqutable 구현

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return EqualityComparer<Cell>.Default.Equals(cell1, cell2);
        }

        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public bool Equals(Cell other)
        {
            return other != null &&
                   _value == other._value;
        }

        public override int GetHashCode()
        {
            return -1939223833 + _value.GetHashCode();
        }

        #endregion

    }


}

