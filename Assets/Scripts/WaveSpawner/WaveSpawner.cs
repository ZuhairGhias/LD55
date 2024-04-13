using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] public int spawnTriggerX;
    [SerializeField] public GameObject[] enemies;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public GameObject cam;
    [SerializeField] public float horizontalSpawnDistance;
    [SerializeField] public float verticalSpawnRange;
    [SerializeField] public Wave[] waves;

    int currentWave;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (currentWave < waves.Length && player.transform.position.x >= waves[currentWave].spawnTriggerX)
        {
            spawnWave(currentWave);
            currentWave++;
        }
    }

    public void spawnWave(int wave)
    {
        foreach (GameObject enemyPrefab in waves[wave].enemies )
        {
            float verticalOffset = Random.Range(-verticalSpawnRange, verticalSpawnRange);
            float horizontalOffset = ( (int)Random.Range(0,2) * horizontalSpawnDistance * 2 ) - horizontalSpawnDistance;

            Vector3 spawnPoint = cam.transform.position + new Vector3( horizontalOffset, verticalOffset, 0 );

            Debug.Log(spawnPoint);

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, player.transform.rotation);
        }
    }
}
