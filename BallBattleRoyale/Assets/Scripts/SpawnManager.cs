using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private readonly float spawnRange = 9.0f;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public int enemyCount;
    public int powerupCount;
    public int waveCount = 1;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    public GameManager gameManager;
    public PlayerController playerController;

    private float timePassed;
    // Start is called before the first frame update
    void Start()
    {       
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        timePassed = 0;
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(), powerupPrefabs[randomPowerup].transform.rotation);

        SpawnWaves(waveCount);   
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(gameManager.isGameActive)
        {
            GameLoop(timePassed);
        }
        
        
    }

    public void GameLoop(float timePass)
    {

        enemyCount = FindObjectsOfType<Enemy>().Length;
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        gameManager.UpdateScore(timePass);
        if (enemyCount == 0)
        {
            waveCount++;
            if (waveCount % bossRound == 0)
            {
                SpawnBossWave(waveCount);
            }
            else
            {
                SpawnWaves(waveCount);
            }
        }
        if (powerupCount == 0 && !playerController.hasPowerup)
        {
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(),
            powerupPrefabs[randomPowerup].transform.rotation);

        }
    }

    private void SpawnWaves(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (enemiesToSpawn < 2)
            {
                Instantiate(enemyPrefabs[0], GenerateSpawnPos(), enemyPrefabs[0].transform.rotation);
            }
            else if (enemiesToSpawn < 5)
            {
                int randomEnemy = Random.Range(0, enemyPrefabs.Length - 1);
                Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPos(), enemyPrefabs[randomEnemy].transform.rotation);
            }
            else
            {
                int randomEnemy = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPos(), enemyPrefabs[randomEnemy].transform.rotation);
            }
            
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new(spawnX, 0, spawnZ);
        return spawnPos;
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateSpawnPos(),
        bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPos(),
            miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }


}
