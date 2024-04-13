using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBase : MobBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Enemy");

        return findClosestOpponentInTargetRange();
    }

    protected override void Attack()
    {
        /*
        Vector3 directionOfTarget = target.transform.position - gameObject.transform.position;
        directionOfTarget.Normalize();
        directionOfTarget *= meleeDistance;

        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, meleeRadius, directionOfTarget);
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<MobBase>().damage(meleeDamage);
            }
        }
        */

        base.Attack();
    }
}
