using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungi : MonoBehaviour, IInteractable, ISpawnable
{
    [SerializeField] private Board _board;
    private Spawner _creator;

    public void OnClick()
    {
        if (_board.CheckBoard())
        {
            Debug.Log("Board done correctly");
        }
        else
        {
            Debug.Log("Board done incorrectly");
        }
        _creator.Create();
        Destroy(gameObject);
    }

    public void OnRelease()
    {
        return;
    }

    public void AssignSpawn(Spawner s)
    {
        _creator = s;
    }
}
