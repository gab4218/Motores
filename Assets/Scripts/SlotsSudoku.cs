using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsSudoku : Slots
{
    [SerializeField] private int[] gridPosition;
    protected override void Trigger(Blocks block)
    {
        board.InputValues(block.value, gridPosition);
    }

    protected override void TriggerExit()
    {
        board.InputValues(0, gridPosition);
    }
}
