using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Gabriel Benes
public class Fungi : MonoBehaviour, IInteractable
{
    [SerializeField] private Board _board;
    private Spawner _creator;
    [SerializeField] private string[] _hints;
    [SerializeField] private TMP_Text _hintText;
    private bool opened = false;

    public void OnClick()
    {
        if (opened) return;

        if (_board.CheckBoard())
        {
            _hintText.text = "You completed the board correctly!";
        }
        else
        {
            if (_board.hintCount < _hints.Length)
            {
                _hintText.gameObject.SetActive(true);
                _hintText.text = _hints[_board.hintCount];
                _board.hintCount++;
                opened = true;
                Invoke("WaitDestroy", 5f);
            }
        }
        
    }

    private void WaitDestroy()
    {
        _hintText.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void OnRelease()
    {
        return;
    }

}


