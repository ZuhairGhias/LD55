using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static InventoryItem;
using static PlayerController;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float moveSpeedX = 10f;
    [SerializeField] public float moveSpeedY = 2f;
    [SerializeField] public float attackDuration = 0.3f;
    [SerializeField] public GameObject attackHitbox;

    private Rigidbody2D rb;

    public static Action<PlayerState> OnPlayerStateChanged;

    private PlayerSummoner summoner;

    private SpriteRenderer sr;

    private float attackTimer;

    [SerializeField]
    public PlayerState _currentState = PlayerState.READY;

    public PlayerState CurrentState
    {
        get
        {
            return _currentState;
        }

        set
        {
            _currentState = value;
            OnPlayerStateChanged?.Invoke(value);
        }
    }

    // Will move the player by vector if player state is ready
    internal void Move(Vector2 vector2)
    {
        if(_currentState == PlayerState.READY)
        {

            sr.flipX = vector2.x < 0 ? true : vector2.x > 0 ? false : sr.flipX;

            rb.MovePosition(rb.position + ( vector2 * new Vector2(moveSpeedX, moveSpeedY) * Time.deltaTime));
        }
        else if (_currentState == PlayerState.ATTACK)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > 0.1f) attackHitbox.SetActive(false);
            if (attackTimer > attackDuration) CurrentState = PlayerState.READY;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DebugUtils.HandleErrorIfNullGetComponent(rb, this);
        summoner = GetComponent<PlayerSummoner>();
        DebugUtils.HandleErrorIfNullGetComponent(summoner, this);
        sr = GetComponent<SpriteRenderer>();
        DebugUtils.HandleErrorIfNullGetComponent(sr, this);
    }

    public void Stage(ItemClass item)
    {
        summoner.StageItem(item);
    }

    public enum PlayerState
    {
        READY,
        WAIT,
        ATTACK
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player collided with "+other);
    }

    public void Attack(float direction)
    {
        if (_currentState == PlayerState.READY)
        {
            CurrentState = PlayerState.ATTACK;
            attackTimer = 0f;

            if (sr.flipX) direction = -1;
            else direction = 1;

            attackHitbox.transform.position = gameObject.transform.position + (Vector3.right * 2.5f * direction);
            attackHitbox.SetActive(true);
        }
    }
}
