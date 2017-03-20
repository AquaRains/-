using System;
using System.Collections;


namespace sudoku
{
    /// <summary>
    /// box값 내부의 한 줄인 3개 한 묶음짜리
    /// </summary>
    public class Shortline : Line, IEnumerable
    {
        private Point[] line = new Point[3];

        /// <summary>
        /// index번째의 point를 반환합니다.
        /// </summary>
        /// <param name="index">인덱스 값</param>
        /// <returns></returns>
        public Point this[int index]
        {
            get => line[index];
            set => line[index] = value;
        }

        /// <summary>
        /// 길이 3의 point 배열을 반환하거나 지정할 수 있습니다.
        /// </summary>
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

        /// <summary>
        /// 3x3 상자에서 number번째의 행 또는 열을 가져옵니다.
        /// </summary>
        /// <param name="box">찾을 box</param>
        /// <param name="number">index</param>
        /// <param name="d">방향</param>
        /// <returns></returns>
        public static Shortline getline(Box box, int number, Direction d)
        {
            Shortline line = new Shortline(d);
            switch (d)
            {
                case Direction.Unabled:
                    throw new ArgumentException("Line 형식만 사용됩니다");

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

            }
            return line;
        }

        /// <summary>
        /// 3x3 배열을 길의 9의 1차원 배열로 변환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="line"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 열거자 : Line의 항목 리턴합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return Line.GetEnumerator();
        }

        /// <summary>
        /// point배열을 shortline으로 변환
        /// </summary>
        /// <param name="v">길이 3의 point 배열</param>
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

