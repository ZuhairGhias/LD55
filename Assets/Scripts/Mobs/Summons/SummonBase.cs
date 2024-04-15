using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBase : MobBase
{
    public AudioClip summonSound;
    public float followRange;

    protected override void Start()
    {
        AudioManager.PlaySoundEffect(summonSound);
        AudioManager.PlaySoundEffect("summon-poof");
        base.Start();
    }

    protected override GameObject chooseTarget()
    {
        opponents = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestOp = findClosestOpponentInTargetRange();
        if (closestOp == null) closestOp = player;

        return closestOp;
    }

    override protected void Move(GameObject target)
    {
        if (target == player)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > followRange)
            {
                Vector3 targetPosition = target.GetComponent<Collider2D>().bounds.center;
                Vector3 movementVector = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                transform.position = movementVector;
            }
            _animator.SetBool("FacingRight", transform.position.x < target.transform.position.x);
            _spriteRenderer.flipX = !_animator.GetBool("FacingRight");
        }
        else base.Move(target);
    }

    override protected void CHASE_UPDATE()
    {
        if (target == player)
        {
            target = chooseTarget();
            Move(target);
            _animator.SetBool("Moving", true);
        }
        else base.CHASE_UPDATE();
    }
}
