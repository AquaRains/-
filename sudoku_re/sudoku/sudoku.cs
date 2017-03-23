using System;
using System.Collections.Generic;

namespace sudoku_re.sudoku
{
    internal interface ICheckAble
    {
        bool checkAvailable(ICheckAble toCheck);
    }

    internal class Sudoku
    {
  
    }

    internal class Point
    {
        private int value = 0;
        private List<int> remains = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        private bool isCustomValue = false;

        public int Value { get { return value; } set { setValue(value); } }

        public bool IsCustomValue { get => isCustomValue; }

        public Point()
        {
        }

        public Point(int value)
        {
            this.value = value;
        }

        private void setValue(int value)
        {
            OnValueChanged.Invoke(this,new PointEventArgs());
            this.value = value; isCustomValue = true;
        }

        public event OnValueChangeHandler OnValueChanged;
        public delegate void OnValueChangeHandler(object o, PointEventArgs e);

        public class PointEventArgs : EventArgs
        {
            public List<int> remains { get; private set; }
            public int value { get; private set; }
            public bool isCustomValue { get; private set; }

            public PointEventArgs(List<int> remains = null, int value = 0, bool? isCustomvalue = false)
            {
                this.remains = remains;
                this.value = value;
                this.isCustomValue = isCustomValue;
            }
        }
    }

    class box : ICheckAble
    {


        public bool checkAvailable(ICheckAble toCheck)
        {
            throw new NotImplementedException();
        }
    }

    abstract class Line<T>
    {

    }
    class ShortLine<T> : Line<T>
    {

    }

    class LongLine<T> : Line<T>, ICheckAble
    {
        public bool checkAvailable(ICheckAble toCheck)
        {
            throw new NotImplementedException();
        }
    }


}