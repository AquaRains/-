using System;
using System.Collections;

namespace Sudoku.Base
{
    public class Board : IEnumerable, ICloneable
    {
        public Box[,] board { get; set; } = new Box[3, 3];

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
                    return board[x, y];
                }
            }
            set
            {
                if (!((x < 0 || x >= 3) && (y < 0 || y >= 3)))
                {
                    board[x, y] = value;
                }
            }
        }



        public Board()
        {
            board = new Box[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = new Box();
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

        public IEnumerator GetEnumerator()
        {
            return board.GetEnumerator();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static explicit operator Board(Box[,] b)
        {
            Board board = new Board()
            {
                board = b
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