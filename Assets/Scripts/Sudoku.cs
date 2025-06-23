using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sudoku : Board
{
    public int[,] boardState;
    public int[,] fullBoard;


    public override bool CheckBoard()
    {
        return boardState == fullBoard;
    }
    public override void InputValues(int val, int[] gridPos)
    {
        boardState[gridPos[0], gridPos[1]] = val;
    }
}
