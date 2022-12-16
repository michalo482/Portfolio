using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private readonly float spawnRange = 9.0f;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new(spawnX, 0, spawnZ);
        return spawnPos;
    }
}
