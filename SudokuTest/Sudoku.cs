using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuTest
{
    class Sudoku
    {
        public int[,] arrayData = new int[9, 9] { 
            {0,0,0,2,6,0,7,0,1}, 
            {6,8,0,0,7,0,0,9,0}, 
            {1,9,0,0,0,4,5,0,0}, 
            {8,2,0,1,0,0,0,4,0},
            {0,0,4,6,0,2,9,0,0},
            {0,5,0,0,0,3,0,2,8},
            {0,0,9,3,0,0,0,7,4},
            {0,4,0,0,5,0,0,3,6},
            {7,0,3,0,1,8,0,0,0}
        };

        const int iteration = 9;

        public Sudoku()
        {

            return;
        }
        public bool SolveGame()
        {
            if(solution(iteration))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check if there is any duplicate number in the same row
        public bool isRowOK(int row, int number)
        {
            for(int i = 0; i < iteration; i++)
            {
                if(arrayData[row, i] == number)
                {
                    return false;
                }
            }
            return true;
        }

        //Check if there is any duplicate number in the same column
        public bool isColumnOK(int col, int number)
        {
            for(int i = 0; i < iteration; i++)
            {
                if(arrayData[i, col] == number)
                {
                    return false;
                }
            }
            return true;
        }

        //Check if there is any duplicate number in the same square
        public bool isSquareOK(int col, int row, int number)
        {
            //Example, row 2, column 5 (we count from 0)
            //Begin row will be 2 - 2 % 3 = 0 (the first row is 0)
            int squareRowBegin = row - row % 3;
            //Begin column will be 5 - 5 % 3 = 3 (the first row of the second quare is 3)
            int squareColumnBegin = col - col % 3;

            for (int i = squareRowBegin; i < squareRowBegin + 3; i++)
            {
                for(int j = squareColumnBegin; j < squareColumnBegin + 3; j++)
                {
                    if(arrayData[i, j] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool solution(int n)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    //To find if there is still unsolved position
                    if(arrayData[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        //Set isEmpty to false because there is a unsolved position
                        isEmpty = false;
                        break;
                    }
                }
                //If find an unsolved position, break the loop to solve it immediately
                if (!isEmpty)
                {
                    break;
                }
            }

            //If there is no more unsolved position, return true
            if(isEmpty)
            {
                return true;
            }

            for (int number = 1; number <= n; number++)
            {
                //Check if there is no duplicate number in the same row, column and square
                if(isRowOK(row, number) 
                    && isColumnOK(col, number) 
                    && isSquareOK(col, row, number))
                {
                    arrayData[row, col] = number;
                    //Keep repeating the process until there is no unsolved position
                    if(solution(n))
                    {
                        return true;
                    }
                    else
                    {
                        arrayData[row, col] = 0;
                    }
                }
            }
            return false;
        }
    }
}
