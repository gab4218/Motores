using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsSudoku : Slots
{
    [SerializeField] private int[] gridPosition;
    protected override void Trigger(Blocks block)
    {
        _board.InputValues(block.value, gridPosition);
    }
}
