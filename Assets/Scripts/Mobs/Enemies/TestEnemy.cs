using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    protected override GameObject chooseTarget()
    {
        return player;
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
