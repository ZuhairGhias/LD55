using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] public float summonTargetRange = 10.0f;
    [SerializeField] public int maxHealth = 100;

    int health;

    GameObject target;
    GameObject player;
    List<GameObject> summons;

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if ( health <= 0 ) die();
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        target = chooseTarget();
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    GameObject chooseTarget()
    {
        return player;
    }

    void die()
    {
        Destroy(gameObject);
    }
}
