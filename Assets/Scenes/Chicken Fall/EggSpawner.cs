using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab; // Reference to the egg prefab
    public GameObject rockPrefab; // Reference to the rock prefab
    public float spawnInterval = 2f; // Time interval between spawns
    public float spawnRange = 4f; // Range for random X position of spawned objects

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // Randomly choose between spawning an egg or a rock
        GameObject objectToSpawn = Random.Range(0f, 1f) > 0.5f ? eggPrefab : rockPrefab;

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
