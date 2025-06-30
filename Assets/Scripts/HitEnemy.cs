using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Andrea Ferruelo
public class HitEnemy : MonoBehaviour
{
    [SerializeField] private int _dmg;
    [SerializeField] private float _knockback;
    private void OnTriggerEnter(Collider other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.DamageEnemy(_dmg, _knockback);
        }
    }
}
