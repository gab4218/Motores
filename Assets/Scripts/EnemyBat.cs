using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Gabriel Benes
public class EnemyBat : EnemyBase
{
    [SerializeField] private Transform _holdPos;
    [SerializeField] private Transform _den;
    [SerializeField] private Transform _target;
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
        else if (_target == _den || _target == null)
        {
            Transform closest = BlockData.placeholder;
            foreach (Transform t in BlockData.blockTransforms)
            {
                if (Vector3.Distance(transform.position, closest.position) > Vector3.Distance(transform.position, t.position) && Vector3.Distance(transform.position, t.position) > 20)
                {
                    closest = t;
                }
            }
            _target = closest;
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

    public override void AssignSpawn(Spawner p)
    {
        base.AssignSpawn(p);
        _den = p.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_holdingBlock || stunned)
        {
            return;
        }

        if (Vector3.Distance(transform.position, _den.position) <= 1f)
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
