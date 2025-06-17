using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : EnemyBase
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _playerDetect;
    private float _webTimer;
    private bool _canAttack;
    private float _cooldown;

    protected override void Start()
    {
        base.Start();
        _webTimer = 0;
        _canAttack = false;
        _cooldown = 0;
    }

    private void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
        if (_cooldown <= 0)
        {
            _cooldown = 0;
            _canAttack = true;
        }
    }

    private void FixedUpdate()
    {
        FindDirection(PlayerActions.instance.transform.position);
        if (Vector3.Distance(PlayerActions.instance.transform.position, transform.position) <= _playerDetect)
        {
            Move(dir);
        }
    }

    private void ApplyEffect()
    {
        if (_cooldown <= 0 && _canAttack == true)
        {
            _webTimer = 30f;
            PlayerActions.instance.GetWebbed(_webTimer);
            _canAttack = false;
        }
    }

    private void ReadyAttack()
    {
        if (_canAttack == false)
        {
            _cooldown = Random.Range(40f, 80f);
        }
    }
}
