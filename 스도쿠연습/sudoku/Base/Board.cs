using System;
using System.Collections;
using System.Collections.Generic;

namespace Sudoku.Base
{
    public class Board : IEnumerable, ICloneable, IEquatable<Board>
    {
        public Box[,] bigBox { get; set; } = new Box[3, 3];

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
                    return bigBox[x, y];
                }
            }
            set
            {
                if (!((x < 0 || x >= 3) && (y < 0 || y >= 3)))
                {
                    bigBox[x, y] = value;
                }
            }
        }        

        public Board()
        {
            bigBox = new Box[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bigBox[i, j] = new Box();
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

        public static explicit operator Board(Box[,] b)
        {
            Board board = new Board()
            {
                bigBox = b
            };
            return board;
        }

        public static explicit operator Box[,] (Board b)
        {
            Box[,] box = b.bigBox;
            return box;
        }



        #region IEnumerable 구현

        public IEnumerator GetEnumerator()
        {
            return bigBox.GetEnumerator();
        }

        #endregion

        #region iCloneable 구현

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion


        #region IEqutable 구현


        public override int GetHashCode()
        {
            return 2119364262 + EqualityComparer<Box[,]>.Default.GetHashCode(bigBox);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Board);
        }

        public bool Equals(Board other)
        {
            return other != null &&
                   EqualityComparer<Box[,]>.Default.Equals(bigBox, other.bigBox);
        }

        public static bool operator ==(Board board1, Board board2)
        {
            return EqualityComparer<Board>.Default.Equals(board1, board2);
        }

        public static bool operator !=(Board board1, Board board2)
        {
            return !(board1 == board2);
        }


        #endregion
    }
}