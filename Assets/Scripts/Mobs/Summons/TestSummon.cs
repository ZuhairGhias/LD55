using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSummon : SummonBase
{
    protected override GameObject chooseTarget()
    {
        return findClosestOpponentInTargetRange();
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
