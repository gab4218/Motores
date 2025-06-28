using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBat : EnemyBase
{
    [SerializeField] private Transform _holdPos;
    [SerializeField] private Transform _den;
    private Transform _target;
    private bool _holdingBlock;
    private bool _stunned;
    private Blocks _selectedBlock;
    private void Update()
    {
        if (_target != PlayerActions.instance.heldItemTransform && PlayerActions.instance.heldItemTransform != null && !_holdingBlock)
        {
            _target = PlayerActions.instance.heldItemTransform;
        }
        else if(_holdingBlock && _selectedBlock.carrierTransform == _holdPos)
        {
            _target = _den;
            if (Vector3.Distance(transform.position, _den.position) <= 0.05f)
            {
                _holdingBlock = false;
                _selectedBlock.isHeld = false;
                _selectedBlock._rb.useGravity = true;
                _selectedBlock = null;
            }
        }
        else if(_holdingBlock)
        {
            _holdingBlock = false;
        }
        if (!_stunned && _target != null)
        {
            FindDirection(_target.position);
            Move(dir);
        }
    }
    public override void Die()
    {
        _holdingBlock = false;
        if (_selectedBlock != null)
        {
            _selectedBlock.carrierTransform = null;
            _selectedBlock.isHeld = false;
            _selectedBlock._rb.useGravity = true;
        }
        base.Die();
    }

    public override void DamageEnemy(int _dmg, float _knockback)
    {
        _holdingBlock = false;
        FindDirection(PlayerActions.instance.transform.position);
        if (_selectedBlock != null)
        {
            _selectedBlock.carrierTransform = null;
            _selectedBlock.isHeld = false;
            _selectedBlock._rb.useGravity = true;
        }
        base.DamageEnemy(_dmg, _knockback);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (_holdingBlock || stunned)
        {
            return;
        }

        if (Vector3.Distance(transform.position, _den.position) <= 0.25f)
        {
            return;
        }

        if (other.TryGetComponent(out Blocks b) && b.canBeHeldByEnemy)
        {
            _selectedBlock = b;
            _selectedBlock.carrierTransform = _holdPos;
            _selectedBlock.isHeld = true;
            _selectedBlock._rb.useGravity = false;
            _holdingBlock = true;
        }
    }
}
