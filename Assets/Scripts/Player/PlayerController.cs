using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static InventoryItem;
using static PlayerController;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 10f;

    private Rigidbody2D rb;

    public static Action<PlayerState> OnPlayerStateChanged;

    private PlayerSummoner summoner;

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
            
            rb.MovePosition(rb.position + ( vector2 * Time.deltaTime * moveSpeed));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DebugUtils.HandleErrorIfNullGetComponent(rb, this);
        summoner = GetComponent<PlayerSummoner>();
        DebugUtils.HandleErrorIfNullGetComponent(summoner, this);
    }

    public void Stage(ItemClass item)
    {
        summoner.StageItem(item);
    }

    public enum PlayerState
    {
        READY,
        WAIT
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player collided with "+other);
    }
}
