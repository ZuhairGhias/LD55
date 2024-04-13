using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MobBase
{
    protected GameObject player;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        base.Start();
    }

    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Summon");

        GameObject closestOp = findClosestOpponentInTargetRange();
        if (closestOp == null) closestOp = player;

        return closestOp;
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
            if (hit.collider.gameObject.tag == "Summon")
            {
                hit.collider.gameObject.GetComponent<MobBase>().damage(meleeDamage);
            }
        }
        */

        base.Attack();
    }
}
