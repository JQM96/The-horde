using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private List<GameObject> objectsToSpawn;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private Vector2 spawnArea;

    private float timer;
    private float spawnTime;
    private Vector2 randomPosition;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            float x = Random.Range(transform.position.x + spawnArea.x / 2, transform.position.x - spawnArea.x / 2);
            float y = Random.Range(transform.position.y + spawnArea.y / 2, transform.position.y - spawnArea.y / 2);
            randomPosition = new Vector2(x, y);

            Spawn(randomPosition);

            timer = 0;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void Spawn(Vector2 position)
    {
        if (objectToSpawn != null)
            Instantiate(objectToSpawn, position, Quaternion.identity, null);

        if (objectsToSpawn.Count != 0)
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Count);
            Instantiate(objectsToSpawn[randomIndex], position, Quaternion.identity, null);
        }
    }
    public void Spawn()
    {
        float x = Random.Range(transform.position.x + spawnArea.x / 2, transform.position.x - spawnArea.x / 2);
        float y = Random.Range(transform.position.y + spawnArea.y / 2, transform.position.y - spawnArea.y / 2);
        randomPosition = new Vector2(x, y);

        if (objectToSpawn != null)
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity, null);

        if (objectsToSpawn.Count != 0)
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Count);
            Instantiate(objectsToSpawn[randomIndex], randomPosition, Quaternion.identity, null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
