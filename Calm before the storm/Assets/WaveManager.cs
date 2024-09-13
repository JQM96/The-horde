using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private List<Spawner> spawners;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int initialEnemyCount;

    private int wave = 1;
    private int currentEnemiesPerWave;
    private float tbwTimer = 0f;
    private bool isWaveActive = false;

    private float currentEnemyCount;
    private float enemiesKilled;

    private void Start()
    {
        currentEnemiesPerWave = initialEnemyCount;
    }

    private void Update()
    {
        if (isWaveActive == false)
        {
            tbwTimer += Time.deltaTime;
            if (tbwTimer >= timeBetweenWaves)
            {
                foreach (Spawner s in spawners)
                {
                    s.gameObject.SetActive(true);
                }

                tbwTimer = 0;
                isWaveActive = true;
            }
        }
        else
        {
            if (currentEnemyCount >= currentEnemiesPerWave)
            {
                foreach (Spawner s in spawners)
                {
                    s.gameObject.SetActive(false);
                }
            }

            if (enemiesKilled >= currentEnemiesPerWave)
            {
                isWaveActive = false;

                enemiesKilled = 0;
                currentEnemyCount = 0;

                currentEnemiesPerWave += (initialEnemyCount / 2);
            }
        }
    }

    public void EnemySpawned()
    {
        currentEnemyCount++;
    }

    public void EnemyKilled()
    {
        currentEnemyCount--;
        enemiesKilled++;
    }
}
