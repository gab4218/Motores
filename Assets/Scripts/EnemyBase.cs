using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour, ISpawnable
{
    [SerializeField] protected int lifeMax;
    [SerializeField] protected float speed;
    protected Transform playerTransform;
    protected Rigidbody rb;
    protected Vector3 facing;
    protected int lifeCurrent;
    protected Vector3 dir;
    protected Spawner creator;
    protected bool stunned = false;
    protected virtual void Start()
    {
        lifeCurrent = lifeMax;
        rb = GetComponent<Rigidbody>();
        playerTransform = PlayerActions.instance.transform;
    }

    public void PermaStun()
    {
        stunned = true;
    }

    public virtual void DamageEnemy(int _dmg, float _knockback)
    {
        lifeCurrent -= _dmg;
        rb.AddForce((transform.up/2 - dir) * _knockback, ForceMode.Impulse);
        stunned = true;
        Invoke("Destun", 0.7f);
        if (lifeCurrent <= 0)
        {
            Die();
        }
    }



    protected void Destun()
    {
        stunned = false;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(creator != null) creator.currentlyAlive--;
    }

    protected virtual void FindDirection(Vector3 position)
    {
        dir = position - transform.position;
        dir.y = 0;
        dir.Normalize();
    }

    protected virtual void Move(Vector3 _dir)
    {
        if (stunned) return;
        rb.velocity = _dir * speed + rb.velocity.y * transform.up;
        facing = Vector3.Lerp(transform.forward, _dir, 0.2f);
        transform.forward = facing;
    }

    public virtual void AssignSpawn(Spawner p)
    {
        creator = p;
    }
}