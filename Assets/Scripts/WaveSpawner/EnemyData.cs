using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 3)]

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public EnemyBase enemyPrefab;
    public string enemyDescription;
}

public enum EnemyType
{
    Garbag,
    Trashcant,
    Mousse,
    Depices,
    BigMac
}