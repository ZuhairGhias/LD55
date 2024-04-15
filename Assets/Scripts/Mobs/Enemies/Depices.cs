using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Depices : EnemyBase
{
    public Projectile EggProjectile;

    protected override void Start()
    {
        base.Start();
        DebugUtils.HandleErrorIfNullGetComponent(EggProjectile, this);
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

            Projectile proj = Instantiate(EggProjectile, transform.position, Quaternion.identity);
            proj.SetTarget(target.GetComponent<Collider2D>().bounds.center, meleeDamage, true);
        }
    }
}
