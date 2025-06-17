using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMimic : EnemyBase, IInteractable
{
    private bool _isDormant;
    private CameraScript _cameraEffect;
    private float _effectTimer;

    protected override void Start()
    {
        base.Start();
        _isDormant = true;
        _effectTimer = 0;
    }

    private void Update()
    {
        if (_isDormant == true)
        {
            speed = 0;
        }
    }

    public void OnClick()
    {
        ApplyEffect(_effectTimer);
        Destroy(gameObject);
    }

    public void OnRelease()
    {
        return;
    }

    private void ApplyEffect(float _effectTime)
    {
        _effectTimer = 10f;
        PlayerActions.instance.GetBlurred(_effectTimer);
    }
}
