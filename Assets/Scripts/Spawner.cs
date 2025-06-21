using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnThing;

    public void Create()
    {
        Instantiate(spawnThing, transform).GetComponent<ISpawnable>().AssignSpawn(this);
    }
}
