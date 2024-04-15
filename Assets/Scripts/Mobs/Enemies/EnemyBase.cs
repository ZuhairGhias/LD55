using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : MobBase
{
    public static Action EnemySpawned;
    public static Action EnemyDestroyed;

    private void Awake()
    {
        EnemySpawned?.Invoke();
    }

    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Summon");

        GameObject closestOp = findClosestOpponentInTargetRange();
        if (closestOp == null) closestOp = player;

        return closestOp;
    }

    protected override void Move(GameObject target)
    {
        base.Move(target);

        // The enemy sprites are reversed
        _spriteRenderer.flipX = _animator.GetBool("FacingRight");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hitbox")
        {
            damage(player.GetComponent<PlayerController>().attackDamage);
            AudioManager.PlaySoundEffect("attack-hit");
            //Debug.Log("Bonked!");
        }

    }

    private void OnDestroy()
    {
        EnemyDestroyed?.Invoke();
    }
}
