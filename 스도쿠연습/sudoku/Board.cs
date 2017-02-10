using System;
using System.Collections;


namespace sudoku
{
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

        public Longline getLongLine(int x, int y, Line.Direction direction)
        {
            switch (direction)
            {
                case Line.Direction.Horizontal:
                    return getLonglineH(x, y);
                case Line.Direction.Vertical:
                    return getLonglineV(x, y);
                default:
                    return null;
            }
        }

        private Longline getLonglineH(int x, int y)
        {
            return (Longline)(new Shortline[]
            {
                this[0, y / 3].Getline(y % 3, Line.Direction.Horizontal),
                this[1, y / 3].Getline(y % 3, Line.Direction.Horizontal),
                this[2, y / 3].Getline(y % 3, Line.Direction.Horizontal)
            });
        }
        private Longline getLonglineV(int x, int y)
        {
            return (Longline)(new Shortline[]
   {
                this[x / 3, 0].Getline(x % 3, Line.Direction.Horizontal),
                this[x / 3, 1].Getline(x % 3, Line.Direction.Horizontal),
                this[x / 3, 2].Getline(x % 3, Line.Direction.Horizontal)
   });
        }


        public bool availableCheck(int[] a)
        {
            throw new NotImplementedException();
        }
        public bool availableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return board.GetEnumerator();
        }

        public static explicit operator Board(Box[,] b)
        {
            Board board = new Board()
            {
                _board = b
            };
            return board;
        }
        public static explicit operator Box[,] (Board b)
        {
            Box[,] box = b.board;
            return box;
        }
    }


}

