using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fungi : MonoBehaviour, IInteractable
{
    [SerializeField] private Board _board;
    private Spawner _creator;
    public static int hintCount = 0;
    [SerializeField] private Dictionary<int, string> _hints;
    [SerializeField] private TMP_Text _hintText;

    public void OnClick()
    {
        if (_board.CheckBoard())
        {
            _hintText.text = "You completed the board correctly!";
        }
        else
        {
            _hintText.text = _hints[hintCount];
        }
        Destroy(gameObject);
    }

    public void OnRelease()
    {
        return;
    }

}
