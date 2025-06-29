using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sudoku : Board
{

    [SerializeField]
    SudokuRow[] rows;

    public int[,] boardState = new int[9, 9]; // cols first, rows second
    public int[,] fullBoard = new int[9, 9];

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                fullBoard[j, i] = rows[i].fullRow[j];
                boardState[j, i] = rows[i].startingRow[j];
            }
        }
        Debug.Log(ReadMatrix(fullBoard));

    }


    //for debugging
    private string ReadMatrix(int[,] m)
    {
        string mat = "";

        for (int i = 0; i < m.GetLength(1); i++)
        {
            for (int j = 0; j < m.GetLength(0); j++)
            {
                mat += m[j, i];
            }
            mat += "\n";
        }
        return mat;
    }



    public override bool CheckBoard()
    {
        Debug.Log(ReadMatrix(boardState));

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (fullBoard[j, i] != boardState[j, i]) return false;
            }
        }

        return true;
    }
    public override void InputValues(int val, int[] gridPos)
    {
        boardState[gridPos[0], gridPos[1]] = val;
        CheckBoard();
    }
}

[System.Serializable]
public struct SudokuRow
{
    public int[] fullRow;
    public int[] startingRow;
}