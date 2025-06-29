using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendoku : Board
{
    public int[] currentVals;
    public int[] fullVals;

    public override bool CheckBoard()
    {
        for(int i = 0; i < currentVals.Length; i++)
        {
            if (currentVals[i] != fullVals[i]) return false;
        }
        return true;
    }



    public override void InputValues(int val, int index)
    {
        currentVals[index] = val;
    }
}
