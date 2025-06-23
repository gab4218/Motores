using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnThing;

    public void Create()
    {
        Instantiate(_spawnThing, transform).GetComponent<ISpawnable>().AssignSpawn(this);
    }
}
