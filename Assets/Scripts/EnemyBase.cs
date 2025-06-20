using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int lifeMax;
    [SerializeField] protected float speed;
    protected Transform playerTransform;
    protected Rigidbody rb;
    protected Vector3 facing;
    protected int lifeCurrent;
    protected Vector3 dir;
    
    protected virtual void Start()
    {
        lifeCurrent = lifeMax;
        rb = GetComponent<Rigidbody>();
        playerTransform = PlayerActions.instance.transform;
    }

    public virtual void DamageEnemy(int _dmg, float _knockback)
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

    protected virtual void FindDirection(Vector3 position)
    {
        dir = position - transform.position;
        dir.y = 0;
        dir.Normalize();
    }

    protected virtual void Move(Vector3 _dir)
    {
        rb.velocity = _dir * speed;
        facing = Vector3.Lerp(transform.forward, _dir, 0.2f);
        transform.forward = facing;
    }
}