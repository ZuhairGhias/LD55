using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MobBase
{
    protected GameObject player;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        base.Start();
    }
}
