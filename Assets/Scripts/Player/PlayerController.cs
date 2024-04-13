using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 10f;

    private CharacterController controller;

    public static Action<PlayerState> OnPlayerStateChanged;

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
            controller.Move(vector2 * Time.deltaTime * moveSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        DebugUtils.HandleErrorIfNullGetComponent(controller, this);
    }

    public enum PlayerState
    {
        READY,
        WAIT
    }
}
