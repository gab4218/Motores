using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendoku : Board
{
    public int[][][] boardState;
    [SerializeField]
    public int[][][] fullBoard { get; private set; }

    public override bool CheckBoard()
    {
        return boardState == fullBoard;
    }

    public override void InputValues(int val, int[] hPos, int[] vPos)
    {
        
    }
}
