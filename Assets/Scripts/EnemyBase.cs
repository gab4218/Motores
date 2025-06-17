using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int lifeMax;
    [SerializeField] protected int lifeCurrent;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected Rigidbody rb;
    protected Vector3 dir;

    private void Awake()
    {
        lifeCurrent = lifeMax;
        rb = GetComponentInChildren<Rigidbody>();
        playerTransform = PlayerActions.instance.transform;
    }

    public void DamageEnemy(int _dmg, float _knockback)
    {
        lifeCurrent -= _dmg;
        rb.AddForce(-dir * _knockback, ForceMode.Impulse);
        if (lifeCurrent <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual Vector3 FindDirection(Vector3 _dir)
    {
        return dir;
    }

    protected virtual void Move(Vector3 _dir)
    {

    }

    protected void OnCollisionEnter(Collision collision)
    {
        
    }
}
