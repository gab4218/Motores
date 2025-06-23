using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendoku : Board
{
    public int[] horizontalsCurrent;
    public int[] verticalsCurrent;
    [SerializeField]
    public int[] horizontalsFull;
    public int[] verticalsFull;

    public override bool CheckBoard()
    {
        return horizontalsCurrent == horizontalsFull && verticalsCurrent == verticalsFull;
    }



    public override void InputValues(int val, bool horizontalVertical, int index)
    {
        
    }
}
