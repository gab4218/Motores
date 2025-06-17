using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : EnemyBase
{
    private float _webTimer;
    private bool _canAttack;
    private float _cooldown;

    private void ApplyEffect()
    {
        PlayerActions.instance.GetWebbed(_webTimer);
    }

    private void ReadyAttack()
    {

    }
}
