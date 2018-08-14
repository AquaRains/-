namespace Sudoku.Base
{

    /// <summary>
    /// 가로, 세로 줄에 관련된 간단한 기본형(인스턴스화 불가)
    /// </summary>
    public abstract class Line : SudokuBase
    {
        /// <summary>
        /// 방향을 나타냅니다. 
        /// 
        /// </summary>
        public enum Direction
        {
            /// <summary> 해당 사항 없음</summary>
            Unabled,
            /// <summary> 가로 </summary>
            Horizontal,
            /// <summary> 세로 </summary>
            Vertical
        }
        /// <summary>
        /// 이 인스턴스에 저장된 방향 값입니다
        /// </summary>
        public Direction direction { get; set; }

    }


}

