using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour, IDamageable
{
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] public float targetRange = 10.0f;
    [SerializeField] public float attackRange = 1.0f;
    [SerializeField] public float attackDuration = 3.0f;
    [SerializeField] public int maxHealth = 100;

    protected MobState state;
    protected GameObject target;
    protected GameObject[] opponents;
    protected float attackTimer;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        switch (state)
        {
            case MobState.CHASE:
                CHASE_UPDATE();
                break;
            case MobState.ATTACK:
                ATTACK_UPDATE();
                break;
        }
        
    }

    void SET_STATE(MobState newState)
    {
        state = newState;

        switch (state)
        {
            case MobState.CHASE:
                CHASE_START();
                break;
            case MobState.ATTACK:
                ATTACK_START();
                break;
        }
    }

    private void CHASE_START()
    {

    }

    private void CHASE_UPDATE()
    {
        target = chooseTarget();
        Move(target);

        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange) SET_STATE(MobState.ATTACK);
    }

    private void ATTACK_START()
    {
        attackTimer = attackDuration;
    }

    private void ATTACK_UPDATE()
    {
        Attack();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) SET_STATE(MobState.CHASE);
    }

    virtual protected GameObject chooseTarget() { return null; } // logic for choosing a target to chase and attack

    protected GameObject findClosestOpponentInTargetRange()
    {
        GameObject closestOpponent = null;
        if (opponents.Length > 0)
        {
            float distanceToClosestOpponent = targetRange;
            float distanceToOp;

            foreach (GameObject op in opponents)
            {
                distanceToOp = Vector3.Distance(gameObject.transform.position, op.transform.position);

                if (distanceToOp < distanceToClosestOpponent)
                {
                    closestOpponent = op;
                    distanceToClosestOpponent = distanceToOp;
                }
            }
        }

        return closestOpponent;
    }

    virtual protected void Move(GameObject target)
    {
        if (target != null) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    virtual protected void Attack() { } // logic for the attack action. will be different for each enemy/summon

    public int health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public void damage(int dmg)
    {
        health -= dmg;
        if (health <= 0) die();
    }

    void die()
    {
        Destroy(gameObject);
    }

    public enum MobState
    {
        CHASE,
        ATTACK
    }
}
