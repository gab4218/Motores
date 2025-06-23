using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Slots : MonoBehaviour
{
    protected Board _board;
    private Blocks _block;
    
    protected void OnTriggerEnter(Collider other)
    {
        if (_block != null) return;

        if (other.TryGetComponent(out Blocks b))
        {
            if (b.type == _board._boardType)
            {
                Trigger(b);
                _block = b;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_block == null) return;
        if (_block == other.GetComponent<Blocks>())
        {
            _block = null;
        }
    }

    protected abstract void Trigger(Blocks block);
}
