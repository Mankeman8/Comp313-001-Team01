using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public GameObject enemyParent;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 30f;
    private float countdown = 10f;
    private float healthValue = 100;
    private int waveIndex = 0;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        Update();
    }
    void Update()
    {
        if (enemiesAlive > 0 || enemyParent.transform.childCount > 0)
        {
            countdown = timeBetweenWaves;
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }
    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        newEnemy.GetComponent<Enemy>().startHealth = newEnemy.GetComponent<Enemy>().startHealth + (healthValue * waveIndex);
        enemiesAlive++;
        newEnemy.transform.parent = enemyParent.transform;
    }
}
