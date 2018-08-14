using System;
using System.Collections;


namespace Sudoku.Base
{
    /// <summary>
    /// box값 내부의 한 줄인 3개 한 묶음짜리
    /// </summary>
    public class Shortline : Line, IEnumerable
    {
        private Point[] line = new Point[3];

        public Point this[int index]
        {
            get => line[index];
            set => line[index] = value;
        }
        public Point[] Line
        {
            get { return line; }
            private set { line = value; }
        }

        Shortline(Box box, int number, Direction d)
        {
            switch (d)
            {
                case Direction.Unabled:
                    throw new ArgumentException("Line 형식만 사용됩니다");
                    break;
                case Direction.Horizontal:
                    Line[0] = box[number, 0];
                    Line[1] = box[number, 1];
                    Line[2] = box[number, 2];
                    break;
                case Direction.Vertical:
                    Line[0] = box[0, number];
                    Line[1] = box[1, number];
                    Line[2] = box[2, number];
                    break;
                default:
                    throw new ArgumentException("잘못된 인수입니다");
                    break;
            }
            direction = d;
        }
        Shortline(Direction d)
        {
            direction = d;
        }
        Shortline()
        {
        }

        public static Shortline getline(Box box, int number, Direction d)
        {
            Shortline line = new Shortline(d);
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
        public static T[,] lineTobox<T>(T[] line)
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


}

