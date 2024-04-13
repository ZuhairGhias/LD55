using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBase : MobBase
{
    void Start()
    {
        opponents = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
