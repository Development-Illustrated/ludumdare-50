using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour
{
    [SerializeField]
    List<Transform> spawnpoints;

    [SerializeField]
    float timeBetweenSpawns;

    [SerializeField] DudeController dudePrefab;

    float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + timeBetweenSpawns;
    }

    void SpawnDude()
    {
        Transform chosenSpawnPoint = spawnpoints[Random.Range(0, spawnpoints.Count)];
        Instantiate(dudePrefab, chosenSpawnPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnDude();
            nextSpawnTime = Time.time + timeBetweenSpawns;
        }
    }
}
