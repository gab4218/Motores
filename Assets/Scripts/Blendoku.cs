using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendoku : Board
{
    public int[] currentVals;
    public int[] fullVals;

    public override bool CheckBoard()
    {
        return currentVals == fullVals;
    }



    public override void InputValues(int val, int index)
    {
        currentVals[index] = val;
    }
}
