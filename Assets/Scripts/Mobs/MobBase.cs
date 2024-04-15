using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour, IDamageable
{
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] public float targetRange = 10.0f;
    [SerializeField] public float attackRange = 1.0f;
    [SerializeField] public float attackDuration = 3.0f;
    [SerializeField] public float maxHealth = 100;

    [SerializeField] public int meleeDamage;

    public AudioClip deathSound;
    public AudioClip hurtSound;

    protected MobState state;
    protected GameObject target;
    protected GameObject[] opponents;
    protected float attackTimer;
    protected float _health;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

    [SerializeField] protected List<Collectible> drops;
    [Range(0.1f, 1f)]
    [SerializeField] protected float dropRate = 0.5f;

    protected GameObject player;

    protected virtual void Start()
    {

        _animator = GetComponent<Animator>();
        DebugUtils.HandleErrorIfNullGetComponent(_animator, this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        DebugUtils.HandleErrorIfNullGetComponent(_animator, this);

        player = GameObject.FindGameObjectWithTag("Player");

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

    virtual protected void CHASE_UPDATE()
    {
        if (target == null || !target.activeSelf)
        {
            target = chooseTarget();
            _animator.SetBool("Moving", false);
        }
        else
        {
            Move(target);
            _animator.SetBool("Moving", true);

            if (Vector3.Distance(transform.position, target.GetComponent<Collider2D>().bounds.center) <= attackRange) SET_STATE(MobState.ATTACK);
        }
    }

    private void ATTACK_START()
    {
        attackTimer = attackDuration;
        if (target == null || !target.activeSelf) target = chooseTarget();
        StartAttackAnim();
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
        if (target != null)
        {
            Vector3 targetPosition = target.GetComponent<Collider2D>().bounds.center;
            targetPosition.y -= (transform.position.y - targetPosition.y) * 3;
            Vector3 movementVector = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = movementVector;
            
            _animator.SetBool("FacingRight", transform.position.x < target.transform.position.x);
            _spriteRenderer.flipX = !_animator.GetBool("FacingRight");
        }

    }

    virtual protected void StartAttackAnim() 
    {
        _animator.SetTrigger("Attack");
    }

    virtual protected void DealAttack()
    {
        // Could have been destroyed
        if (target != null)
        {
            target.GetComponent<IDamageable>()?.damage(meleeDamage);
        }
    }

    public float Health
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

    public void damage(float dmg)
    {
        Health -= dmg;
        _animator.SetTrigger("Damaged");

        AudioManager.PlaySoundEffect(hurtSound);

        if (Health <= 0) die();
    }

    void die()
    {
        AudioManager.PlaySoundEffect(deathSound);

        foreach(Collectible drop in drops)
        {
            if(Random.Range(0, 1) < dropRate)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
        // Death Particle Effect
        Destroy(gameObject);
    }

    public enum MobState
    {
        CHASE,
        ATTACK
    }
}
