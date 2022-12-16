using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab;
    private Vector3 spawnPosition = new(25, 0, 0);
    //private float startDelay = 2;
    private float intervalDelay;
    //private float targetTime;
    private PlayerController playerController;
    public int spawnCount = 0;
    public GameObject[] obstaclePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        //StartCoroutine(RandomWait());
        //InvokeRepeating(nameof(SpawnFirstTime), startDelay, );
        //targetTime = 1.0f;
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        /*targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            SpawnFirstTime();
            targetTime = Random.Range(1.0f, 2.0f);
            
        }*/
    }
    // to ma sie wykonaæ tylko za pierwszym razem
    void SpawnFirstTime()
    {
        if (playerController.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }       
    }
    //rekurencja z randomem
    void SpawnObstacle()
    {
        int obsRand = Random.Range(0, obstaclePrefabs.Length);
        if (spawnCount < 15)
        {
            intervalDelay = Random.Range(1.5f, 2.5f);
        }
        else
        {
            intervalDelay = Random.Range(1.0f, 1.5f);
        }

            if (playerController.gameOver == false)
            {
            Instantiate(obstaclePrefabs[obsRand], spawnPosition, obstaclePrefab.transform.rotation);
                spawnCount++;
            Debug.Log("spawn count " + spawnCount);
            Debug.Log("interval " + intervalDelay);
            }
            Invoke(nameof(SpawnObstacle), intervalDelay);
    }
    //rutyna z wyczekaniem 
    IEnumerator RandomWait()
    {
        while(true)
        {
            if (playerController.gameOver == false)
            {
                Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
            }
            yield return new WaitForSeconds(Random.Range(1.5f, 2.0f));
        }
    }
}
