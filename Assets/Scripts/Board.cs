using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Board : MonoBehaviour, IInteractable
{

    [SerializeField] protected Transform _overviewCameraPosition;
    
    //Referenciar archivo paint pretty please 

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
        set
        {
            return;
        }
    }

    public abstract bool CheckBoard();

    public virtual void InputValues(int val, int[] gridPos)
    {
        Debug.Log("Wrong one bucko");
        return;
    }

    public virtual void InputValues(int val, int[] horizontalPos, int[] verticalPos)
    {
        Debug.Log("Wrong one bucko");
        return;
    }
    
    public void OnClick()
    {
        CameraScript.instance.targetPos = _overviewCameraPosition;
        CameraScript.instance.isOnBoard = true;
    }

    public void OnRelease()
    {
        return;
    }
}
