using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieMonster : SummonBase
{
    public Projectile CookieProjectile;

    protected override void Start()
    {
        base.Start();
        DebugUtils.HandleErrorIfNullGetComponent(CookieProjectile, this);
    }

    override protected void StartAttackAnim()
    {
        _animator.SetTrigger("Attack");
    }

    override protected void DealAttack()
    {
        // Could have been destroyed
        if (target != null)
        {

            Projectile proj = Instantiate(CookieProjectile, transform.position, Quaternion.identity);
            proj.SetTarget(target.GetComponent<Collider2D>().bounds.center, meleeDamage, false);
        }
    }
}
