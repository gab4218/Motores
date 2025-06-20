using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    private Board _board;
    private Blocks _block;
    private int[] _position;
    [SerializeField] public int[] slot { get; }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out Blocks b))
        {
            _block = b;
            
        }
    }
}
