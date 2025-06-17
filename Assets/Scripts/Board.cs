using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : MonoBehaviour
{

    [SerializeField] protected Transform _camera;
    [SerializeField] protected Transform _overviewCameraPosition;

    
    public enum BoardType
    {
        Sudoku,
        Blendoku
    }
    [SerializeField]
    public BoardType _boardType
    { 
        get
        {
            return _boardType;
        }
    }

    public abstract bool CheckBoard();

    public abstract void InputValues<P>(int val);


}
