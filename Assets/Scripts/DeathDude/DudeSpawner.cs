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
        GameObject[] blah = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (GameObject obj in blah)
        {
            spawnpoints.Add(obj.transform);
        }

        // spawnpoints = blah.transformssss;

        nextSpawnTime = Time.time + timeBetweenSpawns;
    }

    void SpawnDude()
    {
        Transform chosenSpawnPoint = spawnpoints[Random.Range(0, spawnpoints.Count)];
        GameObject.Instantiate(dudePrefab, chosenSpawnPoint.position, Quaternion.identity, this.transform);
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
