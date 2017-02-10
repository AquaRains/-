namespace sudoku
{
    interface ICheckable
    {
       bool availableCheck(int boxRownum, int boxColnum, int innerRownum, int innerColnum);
       bool availableCheck(int[] nums);
    }
}
