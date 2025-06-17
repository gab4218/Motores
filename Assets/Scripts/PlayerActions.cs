using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions instance { get; private set; }
    public Player player;
    [SerializeField] private int _dmg;
    [SerializeField] private float _knockback;
    [SerializeField] private float _interactRange;
    [SerializeField] private int lifeMax;
    [SerializeField] private float speed;
    [SerializeField] private float speedSprint;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundRayLength;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private BoxCollider stickCollider;
    [SerializeField] private float frogRayLength;
    private bool _isAttacking;
    private float _attackCooldown;
    private float _webbedTimer;
    private Ray frogRay;
    public enum WeaponType
    {
        Hand, 
        Stick,
        Frog
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        player = new Player(lifeMax, speed, speedSprint, jumpForce, groundRayLength, groundLayerMask, GetComponent<Rigidbody>(), transform);
        player.OnAwake();
        _attackCooldown = 0;
        stickCollider.enabled = false;
    }

    private void Update()
    {
        player.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(weapon);
        }
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        player.OnFixedUpdate();
    }

    private WeaponType weapon;

    private void OnTriggerEnter(Collider other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
        if (enemy != null)
        {
            enemy.DamageEnemy(_dmg, _knockback);
        }
    }

    private void Attack(WeaponType weapon)
    {
        if (_attackCooldown <= 0 && _webbedTimer <= 0)
        {
            if (weapon == WeaponType.Stick)
            {
                stickCollider.enabled = true;
            }
            if (weapon == WeaponType.Frog)
            {
                if (Physics.Raycast(frogRay, out RaycastHit hit, frogRayLength))
                {
                    if (hit.collider.gameObject.TryGetComponent(out EnemyBase enemy))
                    {
                        enemy.Die();
                        _attackCooldown = 10;
                    }
                }
            }
        }
    }

    private void GrabObject()
    {

    }

    public void GetWebbed(float t)
    {
        _webbedTimer = t;
    }

    private void EscapeWeb()
    {

    }
}