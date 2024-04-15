using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public float spawnRate;
    public WaveEnemyInfo[] enemies;
}

[System.Serializable]
public class WaveEnemyInfo
{
    public EnemyData enemyData;
    public int count;
}


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public float horizontalSpawnDistance;
    [SerializeField] public float verticalSpawnRangeHigh;
    [SerializeField] public float verticalSpawnRangeLow;
    [SerializeField] public List<EnemyBase> enemyQueue;

    private float spawnRate;
    private Vector3 spawnLocation;
    private float timer;

    private static WaveSpawner instance;
    private static int enemyCount = 0;

    public static Action WaveDefeated;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        EnemyBase.EnemySpawned += OnEnemySpawn;
        EnemyBase.EnemyDestroyed += OnEnemyDeath;
    }

    public void OnEnemySpawn()
    {
        enemyCount++;
    }

    public void OnEnemyDeath()
    {
        enemyCount--;
        if(!AreThereAnyEnemiesLeft())
        {
            WaveDefeated?.Invoke();
        }
    }

    public bool AreThereAnyEnemiesLeft()
    {
        return enemyQueue.Count > 0 || enemyCount > 0;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (enemyQueue.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                SpawnEnemy();
                timer = 0;
            }
        }
    }

    public void SpawnEnemy()
    {
        if (enemyQueue.Count > 0)
        {
            float verticalOffset = UnityEngine.Random.Range(verticalSpawnRangeLow, verticalSpawnRangeHigh);
            float horizontalOffset = (UnityEngine.Random.Range(0, 2) * horizontalSpawnDistance * 2) - horizontalSpawnDistance;

            Vector3 spawnPoint = spawnLocation + new Vector3(horizontalOffset, verticalOffset, 0);

            Instantiate(enemyQueue[0], spawnPoint, Quaternion.identity);
            enemyQueue.RemoveAt(0); // Remove the prefab we just spawned
        }
    }

    public static void StartWave(WaveData data, Vector3 location)
    {
        instance.spawnRate = data.spawnRate;
        instance.spawnLocation = location;
        foreach(WaveEnemyInfo enemyInfo in data.enemies)
        {
            for(int i = 0; i < enemyInfo.count; i++)
            {
                instance.AddEnemyToQueue(enemyInfo.enemyData);
            }
        }
        instance.ShuffleEnemyQueue();
    }

    private void AddEnemyToQueue(EnemyData enemyData)
    {
        enemyQueue.Add(enemyData.enemyPrefab);
    }

    //Low effort shuffle, should be O(N)
    private void ShuffleEnemyQueue()
    {
        int rand1, rand2;
        EnemyBase temp;
        for (int i = 0;i < enemyQueue.Count; i++)
        {
            rand1 = UnityEngine.Random.Range(0, enemyQueue.Count);
            rand2 = UnityEngine.Random.Range(0, enemyQueue.Count);

            temp = enemyQueue[rand1];
            enemyQueue[rand1] = enemyQueue[rand2];
            enemyQueue[rand2] = temp;
        }
    }

    private void OnDestroy()
    {
        EnemyBase.EnemySpawned -= OnEnemySpawn;
        EnemyBase.EnemyDestroyed -= OnEnemyDeath;
    }
}
