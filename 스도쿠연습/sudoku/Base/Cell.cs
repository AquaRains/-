namespace Sudoku.Base
{


    /// <summary>
    /// 칸 하나에 저장된 값들의 집합입니다.
    /// </summary>
    public class Cell
    {

        /// <summary>
        /// 이곳에 넣을 수 있는 값의 집합
        /// </summary>
        public int[] availableNumList = new int[9];

        private int Value;
        public Cell(int value) { Value = value; }

        public Cell()
        {
            Value = 0;
        }

        /// <summary>
        /// Cell 내부에 저장되어있는 숫자를 나타냅니다
        /// </summary>
        public int value
        {
            get { return Value; }
            set { Value = value; }
        }
        /// <summary>
        /// Cell.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator int(Cell v)
        {
            return v.value;
        }
        /// <summary>
        /// Cell.value값
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator Cell(int v)
        {
            return new Cell(v);
        }

    }


}

