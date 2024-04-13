using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSummon : SummonBase
{
    [SerializeField] public float punchRadius;
    [SerializeField] public float punchDistance;
    [SerializeField] public int dmg;

    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Enemy");

        return findClosestOpponentInTargetRange();
    }

    protected override void Attack()
    {
        // Broke: Using RayCast
        /*
        Vector3 directionOfTarget = target.transform.position - gameObject.transform.position;
        directionOfTarget.Normalize();
        directionOfTarget *= punchDistance;

        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, directionOfTarget);
        if ( hit )
        {
            if ( hit.collider.gameObject.tag == "Enemy" )
            {
                hit.collider.gameObject.GetComponent<MobBase>().damage(dmg);
            }
        }
        */

        // Woke: Using CircleCast
        Vector3 directionOfTarget = target.transform.position - gameObject.transform.position;
        directionOfTarget.Normalize();
        directionOfTarget *= punchDistance;

        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, punchRadius, directionOfTarget);
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log(hit.collider.gameObject.name + " is my target!");
                hit.collider.gameObject.GetComponent<MobBase>().damage(dmg);
            }
        }
    }
}
