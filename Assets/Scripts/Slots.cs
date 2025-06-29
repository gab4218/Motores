using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Slots : MonoBehaviour
{
    public Board board;
    private Blocks _block;
    
    protected void OnTriggerEnter(Collider other)
    {
        if (_block != null) return;

        if (other.TryGetComponent(out Blocks b))
        {
            if (b.type == board._boardType)
            {
                _block = b;
                Trigger(_block);
                
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_block == null) return;

        if (_block == other.GetComponent<Blocks>())
        {
            TriggerExit();
            _block = null;
        }
    }

    protected abstract void Trigger(Blocks block);

    protected abstract void TriggerExit();
}
