using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MobBase
{
    protected GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        opponents = GameObject.FindGameObjectsWithTag("Summon");
    }
}
