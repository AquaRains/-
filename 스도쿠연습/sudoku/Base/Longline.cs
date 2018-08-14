using System;
using System.Collections;


namespace Sudoku.Base
{
    /// <summary>
    /// board 구조에 기초한 9개 한 줄 혹은 3개의 shortline의 한 묶음
    /// </summary>
    public class Longline : Line, IEnumerable
    {
        /// <summary>
        /// 저장된 point[9] 반환
        /// </summary>
        public Cell[] Line { get; private set; } = new Cell[9];
        public int[] LineValues
        {
            get
            {
                int[] res = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    res[i] = this.Line[i].value;
                }
                return res;
            }
        }

        private Shortline[] s3line = new Shortline[3];

        public static Shortline[] GetShortlines(Board board, int number, Direction d)
        {
            Shortline[] lines = new Shortline[3];

            switch (d)
            {
                case Direction.Unabled:
                    throw new ArgumentException();
                case Direction.Horizontal:
                    lines[0] = board[number / 3, 0].Getline(number % 3, Direction.Horizontal);
                    lines[1] = board[number / 3, 1].Getline(number % 3, Direction.Horizontal);
                    lines[2] = board[number / 3, 2].Getline(number % 3, Direction.Horizontal);

                    break;
                case Direction.Vertical:
                    lines[0] = board[0, number / 3].Getline(number % 3, Direction.Vertical);
                    lines[1] = board[1, number / 3].Getline(number % 3, Direction.Vertical);
                    lines[2] = board[2, number / 3].Getline(number % 3, Direction.Vertical);
                    break;
                default:
                    throw new ArgumentException();
            }
            return lines;
        }
        public Shortline[] GetShortlines()
        {
            return s3line;
        }

        public IEnumerator GetEnumerator()
        {
            return Line.GetEnumerator();
        }

        public static explicit operator Longline(Shortline[] v)
        {
            return new Longline(v);

        }

        private void s3to9line()
        {
            for (int i = 0; i < 9; i++)
            {
                Line[i] = s3line[i / 3].Line[i % 3];
            }
        }

        private Longline(Shortline[] v)
        {
            s3line[0] = v[0];
            s3line[1] = v[1];
            s3line[2] = v[2];
            s3to9line();
        }
    }


}

