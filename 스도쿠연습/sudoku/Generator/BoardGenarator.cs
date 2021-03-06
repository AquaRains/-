﻿using System;

namespace sudoku.Generator
{
    internal static class BoardGenarator
    {
        public static Board genarateboard(Board board)
        {
            int[] line = generateLine();
            int[,] firstbox = Shortline.lineTobox(line);
            board[0, 0] = (Box)firstbox;
            int[] line_2 = lineShiftLeft(line);
            int[,] secondbox = Shortline.lineTobox(line_2);
            board[1, 0] = (Box)secondbox;
            int[] line_3 = lineShiftLeft(line_2);
            int[,] thirdbox = Shortline.lineTobox(line_3);
            board[2, 0] = (Box)thirdbox;

            for (int i = 1; i < 3; i++)
            {
                board[0, i].box = boxShiftUp(board[0, i - 1].box);
                board[1, i].box = boxShiftUp(board[1, i - 1].box);
                board[2, i].box = boxShiftUp(board[2, i - 1].box);
            }

            board = (Board)boxrowshuffle((Box[,])board);
            board = (Board)boxcolshuffle((Box[,])board);
            return board;
        }

        private static void swap<T>(ref T A, ref T B)
        {
            T temp = B;
            B = A;
            A = temp;
        }

        private static T[,] boxShiftUp<T>(T[,] input)
        {
            T[,] result = new T[3, 3];

            T[] line_1 = new T[3];
            T[] line_2 = new T[3];
            T[] line_3 = new T[3];

            for (int i = 0; i < 3; i++)
            {
                line_1[i] = input[0, i];
                line_2[i] = input[1, i];
                line_3[i] = input[2, i];
            }
            for (int i = 0; i < 3; i++)
            {
                result[0, i] = line_2[i];
                result[1, i] = line_3[i];
                result[2, i] = line_1[i];
            }
            return result;
        }

        private static T[,] boxrowshuffle<T>(T[,] input)
        {
            return boxShuffle(input, Line.Direction.Vertical);
        }

        private static T[,] boxcolshuffle<T>(T[,] input)
        {
            return boxShuffle(input, Line.Direction.Horizontal);
        }

        private static T[,] boxShuffle<T>(T[,] input, Line.Direction direction)
        {
            int l, r;
            l = r = 1;
            l *= direction == Line.Direction.Horizontal ? 1 : 0;
            r *= direction == Line.Direction.Vertical ? 1 : 0;
            if (l * r > 0) return input;

            T[,] result = new T[3, 3];
            T[][] lines = new T[3][];
            Random ran = new Random(DateTime.Now.Millisecond);

            lines[0] = new T[3] { input[0 * l, 0 * r], input[1 + 0 * l, 1 + 0 * r], input[2 + 0 * l, 2 + 0 * r] };
            lines[1] = new T[3] { input[1 * l, 1 * r], input[1 + 1 * l, 1 + 1 * r], input[2 + 1 * l, 2 + 1 * r] };
            lines[2] = new T[3] { input[2 * l, 2 * r], input[1 + 2 * l, 1 + 2 * r], input[2 + 2 * l, 2 + 2 * r] };

            for (int i = 0; i < 3; i++)
            {
                int ranA = ran.Next(3);
                int ranB = ran.Next(3);
                swap(ref lines[ranA], ref lines[ranB]);
            }

            for (int i = 0; i < 3; i++)
            {
                result[0, i] = lines[i * l + 0 * r][0 * l + i * r];
                result[1, i] = lines[i * l + 1 * r][1 * l + i * r];
                result[2, i] = lines[i * l + 2 * r][2 * l + i * r];
            }

            return result;
        }

        private static T[] lineShiftLeft<T>(T[] input)
        {
            T[] line_temp = new T[8];
            Array.Copy(input, 1, line_temp, 0, 8);
            T[] line = new T[9];
            line_temp.CopyTo(line, 0);
            line[8] = input[0];

            return line;
        }

        private static int[] generateLine()
        {
            Random rd = new Random(DateTime.Now.Millisecond);

            int[] result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = i + 1;
            }

            for (int i = 0; i < 18; i++)
            {
                int a = rd.Next(9);  // 0~9 난수발생해서 아무렇게나 자리를 바꾼다
                int b = rd.Next(9);

                int temp = result[a];
                result[a] = result[b];
                result[b] = temp;
            }
            return result;
        }
    }
}