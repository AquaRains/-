using System;
using System.Collections;
using System.Linq;


namespace sudoku
{
    interface ICheckable
    {
        bool AvailableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum);
        bool AvailableCheck(int[] a);
    }

    /// <summary>
    /// 그 점 하나에 저장된 값들의 집합입니다.
    /// </summary>
    public class Point
    {

        private int Value;
        /// <summary>
        /// 이곳에 넣을 수 있는 값의 집합
        /// </summary>
        public int[] availableNumList = new int[9];
        /// <summary>
        /// point 내부에 저장되어있는 숫자를 나타냅니다
        /// </summary>
        public int value
        {
            get { return Value; }
            set { Value = value; }
        }

        public Point(int value)
        {
            Value = value;
        }
        public Point()
        {
            Value = 0;
        }

        /// <summary>
        /// point.value값
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator int(Point v)
        {
            return v.value;
        }
        /// <summary>
        /// point.value값
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Point(int v)
        {
            return new Point(v);
        }

    }

    /// <summary>
    /// 가로, 세로 줄에 관련된 간단한 기본형(인스턴스화 불가)
    /// </summary>
    public abstract class line
    {
        /// <summary>
        /// 방향을 나타냅니다. 
        /// 
        /// </summary>
        public enum Direction
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
        private Direction _Direction;
        /// <summary>
        /// 이 인스턴스에 저장된 방향 값입니다
        /// </summary>
        public Direction direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }


    }
    /// <summary>
    /// box값 내부의 한 줄인 3개 한 묶음짜리
    /// </summary>
    public class Shortline : line, IEnumerable
    {

        private Point[] line = new Point[3];
        public Point[] Line
        {
            get { return line; }
            private set { line = value; }
        }

        Shortline(Box box, int number, Direction d)
        {
            this.Line = Getline(box, number, d);
            direction = d;
        }
        Shortline(Direction d)
        {
            direction = d;
        }
        Shortline()
        {
        }

        public static Point[] Getline(Box box, int number, Direction d)
        {
            Point[] line = new Point[3];
            switch (d)
            {
                case Direction.Unabled:
                    throw new ArgumentException("Line 형식만 사용됩니다");
                    break;
                case Direction.Horizontal:
                    line[0] = box[number, 0];
                    line[1] = box[number, 1];
                    line[2] = box[number, 2];
                    break;
                case Direction.Vertical:
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

        public IEnumerator GetEnumerator()
        {
            return Line.GetEnumerator();
        }

        public static explicit operator Shortline(Point[] v)
        {
            Shortline temp = new Shortline();
            temp.Line[0] = v[0];
            temp.Line[1] = v[1];
            temp.Line[2] = v[2];
            return temp;
        }

    }
    /// <summary>
    /// board 구조에 기초한 9개 한 줄 혹은 3개의 shortline의 한 묶음
    /// </summary>
    public class Longline : line,IEnumerable
    {
        private Shortline[] s3line = new Shortline[3];
        private Point[] line = new Point[9];
        private void s3to9line()
        {
            for(int i = 0; i < 9; i++)
            {
                line[i] = s3line[i / 3].Line[i % 3];
            }
        }
        /// <summary>
        /// 저장된 point[9] 반환
        /// </summary>
        public Point[] Line
        {
            get { return line; }
            private set { line = value; }
        }

        public static Shortline[] Getline(Board board, int number, Direction d)
        {
            Shortline[] lines = new Shortline[3];

            switch (d)
            {
                case Direction.Unabled:
                    throw new ArgumentException();
                case Direction.Horizontal:
                    lines[0] = (Shortline)board[number / 3, 0].Getline(number % 3, Direction.Horizontal);
                    lines[1] = (Shortline)board[number / 3, 1].Getline(number % 3, Direction.Horizontal);
                    lines[2] = (Shortline)board[number / 3, 2].Getline(number % 3, Direction.Horizontal);

                    break;
                case Direction.Vertical:
                    lines[0] = (Shortline)board[0, number / 3].Getline(number % 3, Direction.Vertical);
                    lines[1] = (Shortline)board[1, number / 3].Getline(number % 3, Direction.Vertical);
                    lines[2] = (Shortline)board[2, number / 3].Getline(number % 3, Direction.Vertical);
                    break;
                default:
                    throw new ArgumentException();
            }
            return lines;
        }

        public IEnumerator GetEnumerator()
        {
            return Line.GetEnumerator();
        }

        public static explicit operator Longline(Shortline[] v)
        {
            return new Longline(v);

        }

        private Longline(Shortline[] v)
        {
            s3line[0] = v[0];
            s3line[1] = v[1];
            s3line[2] = v[2];
            s3to9line();
        }
    }
    public class Box : ICheckable, IEnumerable
    {
        public readonly int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private Point[,] _box;
        public Point[,] box
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


        protected int[] BoxtoLine()
        {
            Point[] point = BoxtoLine(_box);
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
        public Point[] Getline(int number, line.Direction d)
        {
            return Shortline.Getline(this, number, d);
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
    
    public class Board : ICheckable, IEnumerable
    {
        Box[,] _board = new Box[3, 3];
        public Box this[int x, int y]
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

        public Box[,] board
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

        public Board()
        {
            _board = new Box[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = new Box();
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

        public IEnumerator GetEnumerator()
        {
            return board.GetEnumerator();
        }

        public static explicit operator Board(Box[,] b)
        {
            Board board = new Board();
            board._board = b;
            return board;
        }
        public static explicit operator Box[,] (Board b)
        {
            Box[,] box = b.board;
            return box;
        }
    }


}

