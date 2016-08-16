using System;
using System.Linq;


namespace sudoku
{
    interface ICheckable
    {
        bool AvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum);
        bool AvailableCheck(int[] a);
    }

    public class point
    {
        public int innerRownum, innerColnum;
        private int Value;
        public int[] availableNumList = new int[9];

        public int value
        {
            get { return Value; }
            set { Value = value; }
        }
        public point(int InnerRownum, int InnerColnum, int value)
        {
            innerRownum = InnerRownum;
            innerColnum = InnerColnum;
            Value = value;
        }
        public point(int InnerRownum, int InnerColnum)
        {
            innerColnum = InnerColnum;
            innerRownum = InnerRownum;
            Value = 0;
        }
        public point(int value)
        {
            Value = value;
        }
        public point()
        {
            Value = 0;
        }

        public static explicit operator int(point v)
        {
            return v.value;
        }
        public static explicit operator point(int v)
        {
            return new point(v);
        }


    }
    public abstract class line
    {
        /// <summary>
        /// 방향을 나타냅니다. 
        /// 
        /// </summary>
        public enum direction
        {
            /// <summary>
            /// 해당 사항 없음
            /// </summary>
            Unabled,
            /// <summary>
            /// 가로
            /// </summary>
            Horizontal,
            /// <summary>
            /// 세로
            /// </summary>
            Vertical
        }
        private direction _Direction;
        /// <summary>
        /// 이 인스턴스에 저장된 방향 값입니다
        /// </summary>
        public direction Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }


    }
    public class shortline : line
    {

        private point[] line = new point[3];
        public point[] Line
        {
            get { return line; }
            private set { line = value; }
        }

        shortline(box box, int number, direction d)
        {
            this.Line = Getline(box, number, d);
            Direction = d;
        }
        shortline(direction d)
        {
            Direction = d;
        }
        shortline()
        {
        }

        public static point[] Getline(box box, int number, direction d)
        {
            point[] line = new point[3];
            switch (d)
            {
                case direction.Unabled:
                    throw new ArgumentException("Line 형식만 사용됩니다");
                    break;
                case direction.Horizontal:
                    line[0] = box[number, 0];
                    line[1] = box[number, 1];
                    line[2] = box[number, 2];
                    break;
                case direction.Vertical:
                    line[0] = box[0, number];
                    line[1] = box[1, number];
                    line[2] = box[2, number];
                    break;
                default:
                    throw new ArgumentException("잘못된 인수입니다");
                    break;
            }
            return line;
        }
        public static T[,] LineTobox<T>(T[] line)
        {
            T[,] box = new T[3, 3];
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
        public static explicit operator shortline(point[] v)
        {
            shortline temp = new shortline();
            temp.Line[0] = v[0];
            temp.Line[1] = v[1];
            temp.Line[2] = v[2];
            return temp;
        }

    }
    public class longline : line
    {
        private shortline[] s3line = new shortline[3];
        private point[] line = new point[9];
        private void s3to9line()
        {
            for(int i = 0; i < 9; i++)
            {
                line[i] = s3line[i / 3].Line[i % 3];
            }
        }
        public point[] Line
        {
            get { return line; }
            private set { line = value; }
        }

        public static shortline[] Getline(board board, int number, direction d)
        {
            shortline[] lines = new shortline[3];

            switch (d)
            {
                case direction.Unabled:
                    throw new ArgumentException();
                case direction.Horizontal:
                    lines[0] = (shortline)board[number / 3, 0].Getline(number % 3, direction.Horizontal);
                    lines[1] = (shortline)board[number / 3, 1].Getline(number % 3, direction.Horizontal);
                    lines[2] = (shortline)board[number / 3, 2].Getline(number % 3, direction.Horizontal);

                    break;
                case direction.Vertical:
                    lines[0] = (shortline)board[0, number / 3].Getline(number % 3, direction.Vertical);
                    lines[1] = (shortline)board[1, number / 3].Getline(number % 3, direction.Vertical);
                    lines[2] = (shortline)board[2, number / 3].Getline(number % 3, direction.Vertical);
                    break;
                default:
                    throw new ArgumentException();
            }
            return lines;
        }
        public static explicit operator longline(shortline[] v)
        {
            return new longline(v);

        }

        private longline(shortline[] v)
        {
            s3line[0] = v[0];
            s3line[1] = v[1];
            s3line[2] = v[2];
            s3to9line();
        }
    }
    public class box : ICheckable
    {
        public readonly int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private point[,] _box;
        public point[,] Box
        {
            get { return _box; }
            set { _box = value; }
        }

        public bool Available
        {
            get
            {
                return AvailableCheck();
            }
        }
        public point this[int x, int y]
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



        public box()
        {

            _box = new point[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _box[i, j] = new point(i, j);
                }
            }

        }


        protected int[] BoxtoLine()
        {
            point[] point = BoxtoLine(_box);
            int[] result = new int[point.Length];
            for (int a = 0; a < point.Length; a++)
            {
                result[a] = (int)point[a];
            }
            return result;
        }
        public static T[] BoxtoLine<T>(T[,] box)
        {
           T[] line = new T[9];

            for (int i = 0; i < 9; i++)
            {
                line[i] = box[i % 3, i / 3];
            }

            return line;
        }
        public point[] Getline(int number, line.direction d)
        {
            return shortline.Getline(this, number, d);
        }

        

        #region Availablecheck

        public bool AvailableCheck(int innerRownum, int innerColnum)
        {
            int a = _box[innerRownum, innerColnum].value;
            return a == 0 || Array.FindAll(BoxtoLine(_box), x => (int)x == a).Count() <= 1;
        }
        public bool AvailableCheck(int[] a)
        {
            return AvailableCheck(a[0], a[1], a[2], a[3]);
        }
        public bool AvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            return AvailableCheck(innerRownum, innerColnum);
        }
        public bool AvailableCheck()
        {
            int[] temp = BoxtoLine();
            Array.Sort(temp);
            return temp == nums;
        } 
        #endregion

        public static explicit operator box(int[,] v)
        {
            box box = new box();
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    box.Box[i,j].value = v[i, j];
                }
            }
            return box;
        }
      
       
    }

    public class board : ICheckable
    {
        box[,] _board = new box[3, 3];
        public box this[int x, int y]
        {
            get
            {
                if ((x < 0 || x >= 3) && (y < 0 || y >= 3))
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return _board[x, y];
                }
            }
            set
            {
                if (!((x < 0 || x >= 3) && (y < 0 || y >= 3)))
                {
                    _board[x, y] = value;
                }
            }
        }

        public box[,] Board
            {
            get
            {
                return _board;
            }
            set
            {
                _board = value;
            }
            }

        public board()
        {
            _board = new box[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = new box();
                }
            }
        }


        public bool AvailableCheck(int[] a)
        {
            throw new NotImplementedException();
        }
        public bool AvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            throw new NotImplementedException();
        }

        public static explicit operator board(box[,] b)
        {
            board board = new board();
            board._board = b;
            return board;
        }
        public static explicit operator box[,] (board b)
        {
            box[,] box = b.Board;
            return box;
        }
    }


}

