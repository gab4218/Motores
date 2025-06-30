using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Gabriel Benes
public abstract class Board : MonoBehaviour, IInteractable
{

    [SerializeField] protected Transform _overviewCameraPosition;
    [SerializeField] protected MeshRenderer _winMeshRenderer;
    [SerializeField] protected MeshRenderer _thisMR;
    [SerializeField] protected Animator _winAnim;
    [SerializeField] protected Material _winMaterial;
    //Referenciar archivo paint pretty please 
    public int hintCount = 0;
    protected bool _completed;
    public enum BoardType
    {
        Sudoku,
        Blendoku
    }
    public BoardType _boardType;

    protected virtual void Awake()
    {
        _thisMR = GetComponent<MeshRenderer>();
        CheckButton.onPress += LostThisOne;
    }

    public abstract bool CheckBoard();
    protected virtual void Update()
    {
        if (!_completed)
        {
            if (CheckBoard() == true)
            {
                _completed = true;
                CheckButton.onPress -= LostThisOne;
                CheckButton.onPress += WinThisOne;
                Debug.Log("WonThisOne");
            }
        }
    }

    private void WinThisOne()
    {
        _winMeshRenderer.material = _winMaterial;
        _winAnim.SetTrigger("win");
    }
    private void LostThisOne()
    {
        _thisMR.material.color = new Color(1f, 0.2f, 0.2f);
        Invoke("BackToNormal", 1.0f);
    }

    private void BackToNormal()
    {
        _thisMR.material.color = Color.white;
    }

    public virtual void InputValues(int val, int[] gridPos)
    {
        Debug.Log("Wrong one bucko");
        return;
    }


    public virtual void InputValues(int val, int index)
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
