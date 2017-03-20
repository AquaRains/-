using System;
using System.Collections;
using System.Linq;


namespace sudoku
{
    public class Box : ICheckable, IEnumerable
    {
        public readonly int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private Point[,] _box;
        public Point[,] box
        {
            get { return _box; }
            set { _box = value; }
        }

        public bool available => availableCheck();
        public Point this[int x, int y]
        {
            get
            {
                if ((x < 0 || x >= 3) && (y < 0 || y >= 3))
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return _box[x, y];
                }
            }
            set
            {
                if (!((x < 0 || x >= 3) && (y < 0 || y >= 3)))
                {
                    _box[x, y] = value;
                }
            }
        }

        public Box()
        {

            _box = new Point[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _box[i, j] = new Point();
                }
            }

        }


        protected int[] boxtoLine()
        {
            Point[] point = boxtoLine(_box);
            int[] result = new int[point.Length];
            for (int a = 0; a < point.Length; a++)
            {
                result[a] = (int)point[a];
            }
            return result;
        }
        public static T[] boxtoLine<T>(T[,] box)
        {
            T[] line = new T[9];

            for (int i = 0; i < 9; i++)
            {
                line[i] = box[i % 3, i / 3];
            }

            return line;
        }
        public Shortline Getline(int number, Line.Direction d)
        {
            return Shortline.getline(this, number, d);
        }



        #region Availablecheck

        public bool availableCheck(int innerRownum, int innerColnum)
        {
            int a = _box[innerRownum, innerColnum].value;
            return a == 0 || Array.FindAll(boxtoLine(_box), x => (int)x == a).Count() <= 1;
        }
        public bool availableCheck(int[] a)
        {
            return availableCheck(a[0], a[1], a[2], a[3]);
        }
        public bool availableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            return availableCheck(innerRownum, innerColnum);
        }
        public bool availableCheck()
        {
            int[] temp = boxtoLine();
            Array.Sort(temp);
            return temp == nums;
        }

        public IEnumerator GetEnumerator()
        {
            return box.GetEnumerator();
        }
        #endregion
        public static explicit operator Box(int[,] v)
        {
            Box box = new Box();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    box.box[i, j].value = v[i, j];
                }
            }
            return box;
        }
    }


}

