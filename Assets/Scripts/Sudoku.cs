using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sudoku : Board
{

    [SerializeField]
    SudokuRow[] rows;

    public int[,] boardState;
    public int[,] fullBoard;

    protected override void Awake()
    {
        //for ()
        {

        }
    }


    public override bool CheckBoard()
    {
        return boardState == fullBoard;
    }
    public override void InputValues(int val, int[] gridPos)
    {
        boardState[gridPos[0], gridPos[1]] = val;
    }
}
public struct SudokuRow
{
    public int[] fullRow;
    public int[] startingRow;
}