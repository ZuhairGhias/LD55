using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Summon");

        GameObject closestOp = findClosestOpponentInTargetRange();
        if (closestOp == null) closestOp = player;

        return closestOp;
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
