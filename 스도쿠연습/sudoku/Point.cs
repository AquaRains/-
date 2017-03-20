namespace sudoku
{


    /// <summary>
    /// 그 점 하나에 저장된 값들의 집합입니다.
    /// </summary>
    public class Point
    {

        /// <summary>
        /// 이곳에 넣을 수 있는 값의 집합
        /// </summary>
        public int[] availableNumList = new int[9];

        private int Value;
        public Point(int value) { Value = value; }

        public Point()
        {
            Value = 0;
        }

        /// <summary>
        /// point 내부에 저장되어있는 숫자를 나타냅니다
        /// </summary>
        public int value
        {
            get { return Value; }
            set { Value = value; }
        }
        /// <summary>
        /// point.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator int(Point v)
        {
            return v.value;
        }
        /// <summary>
        /// point.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator Point(int v)
        {
            return new Point(v);
        }

    }


}

