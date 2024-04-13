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

    [SerializeField] public int meleeDamage;

    protected MobState state;
    protected GameObject target;
    protected GameObject[] opponents;
    protected float attackTimer;
    protected int _health;

    protected virtual void Start()
    {
        Health = maxHealth;

        SET_STATE(MobState.CHASE);
    }

    protected virtual void Update()
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
        target = chooseTarget();
    }

    private void CHASE_UPDATE()
    {
        if (target == null || !target.activeSelf) target = chooseTarget();
        else
        {
            Move(target);

            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange) SET_STATE(MobState.ATTACK);
        }
    }

    private void ATTACK_START()
    {
        attackTimer = attackDuration;
        if (target == null || !target.activeSelf) target = chooseTarget();
        Attack();
    }

    private void ATTACK_UPDATE()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) SET_STATE(MobState.CHASE);
    }

    virtual protected GameObject chooseTarget() { return null; }

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

    virtual protected void Attack() 
    {
        target.GetComponent<MobBase>().damage(meleeDamage);
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public void damage(int dmg)
    {
        Health -= dmg;
        if (Health <= 0) die();
        Debug.Log(gameObject.name + " has been struck!");
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
