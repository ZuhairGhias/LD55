using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    public static Action CheckpointReached;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Checkpoint Reached");
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            CheckpointReached?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
