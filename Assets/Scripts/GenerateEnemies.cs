using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    //What enemy we spawning
    public GameObject enemy;
    //timer countdown until next spawn
    public float timer;
    //where will they spawn?
    public GameObject enemySpawner;
    public int enemyCount = 0;
    public bool spawn = false;
    public bool levelDone = false;
    private int totalCount;
    //private to keep track of stuff
    private Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Element0 is the parent object
        //Random.Range uses 1 so we don't use the parent object
        spawnPoints = GetComponentsInChildren<Transform>();
        spawn = true;
        switch (FindObjectOfType<GameManager>().GetDifficulty())
        {
            case "Easy":
                totalCount = 50;
                break;
            case "Normal":
                totalCount = 100;
                break;
            case "Hard":
                totalCount = 150;
                break;
            default:
                totalCount = 50;
                break;
        }
        StartCoroutine(EnemyDrop());
    }

    private void FixedUpdate()
    {
        if(spawn)
        {
            StartCoroutine(EnemyDrop());
        }
        if(levelDone && enemyCount <= 0)
        {
            FindObjectOfType<GameManager>().LevelOne();
        }
    }

    IEnumerator EnemyDrop()
    {
        if(spawn && enemyCount < totalCount)
        {
            int spawnLocation = Random.Range(1, spawnPoints.Length - 1);
            Vector3 position = new Vector3(spawnPoints[spawnLocation].transform.position.x, spawnPoints[spawnLocation].transform.position.y + 25f, spawnPoints[spawnLocation].transform.position.z);
            Instantiate(enemy, position, Quaternion.identity,spawnPoints[spawnLocation].transform);
            enemyCount++;
            yield return new WaitForSeconds(timer);
        }
    }
}
