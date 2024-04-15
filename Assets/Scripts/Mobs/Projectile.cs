using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int damage;
    bool targetFriendly;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public virtual void SetTarget(Vector3 position, int damage, bool targetFriendly, float speed = 1, bool arc = false)
    {
        // Shoot off into the distance
        Vector3 actualTarget = transform.position + ((position - transform.position).normalized * 10);
        float duration = 1 / speed;
        this.damage = damage;
        this.targetFriendly = targetFriendly;
        gameObject.transform.DOMove(actualTarget, duration);
        transform.DORotate(new Vector3(0, 0, 90), duration);
        if (arc)
        {
            transform.DOPunchPosition(Vector3.up, duration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            if ((!targetFriendly && target is EnemyBase) || (targetFriendly && target is not EnemyBase))
            {
                target.damage(damage);
                Destroy(gameObject);
            }

        }
    }

    public void OnDestroy()
    {
        transform.DOKill();
    }
}
