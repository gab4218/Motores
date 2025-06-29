using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnThing;
    [SerializeField] private int _maxAlive;
    [SerializeField] private float _maxCooldown;
    private float _currentCooldown;
    public int currentlyAlive;
    

    private void Update()
    {
        if (currentlyAlive < _maxAlive)
        {
            if (_currentCooldown >= _maxCooldown)
            {
                Create();
                _currentCooldown = 0;
            }
            else
            {
                _currentCooldown += Time.deltaTime;
            }
        }
    }

    public void Create()
    {
        currentlyAlive++;
        Instantiate(_spawnThing, transform.position, Quaternion.identity).GetComponent<ISpawnable>().AssignSpawn(this);
    }
}
