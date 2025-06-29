using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsBlendoku : Slots
{
    [SerializeField] private int index;
    protected override void Trigger(Blocks block)
    {
        board.InputValues(block.value, index);
    }

    protected override void TriggerExit()
    {
        board.InputValues(0, index);
    }
}
