using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private AudioClip nextWaveClip;

    private int wave = 0;
    private int currentEnemiesPerWave;
    private float tbwTimer = 0f;
    private bool isWaveActive = false;

    private int currentEnemyCount;
    private int enemiesKilled;

    private void Start()
    {
        currentEnemiesPerWave = initialEnemyCount;

        waveText.text = "Wave: " + wave.ToString("0000");
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
                enemiesKilled = 0;
                currentEnemyCount = 0;

                if (wave != 0)
                    currentEnemiesPerWave += (initialEnemyCount / 2);

                wave++;
                waveText.text = "Wave: " + wave.ToString("0000");

                AudioManager.PlaySound(nextWaveClip);
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

                currentEnemiesPerWave = currentEnemyCount;
            }

            if (enemiesKilled >= currentEnemiesPerWave)
                isWaveActive = false;
        }
    }

    public void EnemySpawned()
    {
        currentEnemyCount++;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }
}
