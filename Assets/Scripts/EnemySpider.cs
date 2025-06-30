using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Andrea Ferruelo
public class EnemySpider : EnemyBase
{
    [SerializeField] private Transform _nest;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _playerDetect;
    [SerializeField] private float _webTimer;
    [SerializeField] private float _maxCooldown;
    [SerializeField] private Collider _webCollider;
    private float _cooldown;
    private bool _canAttack;
    [SerializeField] private bool _chasingPlayer = false;

    protected override void Start()
    {
        base.Start();
        
        _canAttack = true;
        _cooldown = 0;
        _webCollider.enabled = false;
    }

    private void Update()
    {
        if (CameraScript.instance.isOnBoard) return;

        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
        else if (_cooldown < 0)
        {
            _cooldown = 0;
            _canAttack = true;
        }

        if (Vector3.Distance(PlayerActions.instance.transform.position, transform.position) <= _attackRange && _canAttack && !stunned)
        {
            _webCollider.enabled = true;
        }
    }

    

    private void FixedUpdate()
    {
        if (CameraScript.instance.isOnBoard) return;

        if (_chasingPlayer)
        {
            FindDirection(PlayerActions.instance.transform.position);

            Move(dir);
        }
        else
        {
            FindDirection(_nest.position);
            Move(dir);
            if (PlayerActions.instance.webbedTimer > 0 && Vector3.Distance(PlayerActions.instance.transform.position, transform.position) <= _playerDetect)
            {
                PlayerActions.instance.player.rb.velocity = dir * speed;
            }
            else
            {
                _chasingPlayer = true;
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canAttack && _webCollider.enabled)
        {
            if(other.GetComponent<PlayerActions>())
            {
                _cooldown = _maxCooldown;
                ApplyEffect();
                _webCollider.enabled = false;
            }
        }
    }

    public override void AssignSpawn(Spawner p)
    {
        base.AssignSpawn(p);
        _nest = p.transform;
    }

    private void ApplyEffect()
    {
        if (_canAttack == true)
        {
            PlayerActions.instance.GetWebbed(_webTimer);
            _canAttack = false;
            _chasingPlayer = false;
        }
    }
}
