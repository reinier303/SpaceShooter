using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance;

    [HideInInspector]
    public int EnemiesAlive = 0;

    public Wave[] Waves;

    public static bool Spawning = true;

    [HideInInspector]
    public int enemiesToSpawn;

    public static int waveIndex = 0;

    ObjectPooler objectPooler;

    GameManager gameManager;

    public Wave CurrentWave;

    public GameObject Map;

    public SpriteRenderer MapRenderer;

    EnemyManager enemyManager;

    UIManager uIManager;

    private void Awake()
    {
        Instance = this;
        objectPooler = ObjectPooler.Instance;
        gameManager = GameManager.Instance;
        CurrentWave = Waves[0];
    }

    private void Start()
    {
        enemyManager = EnemyManager.Instance;
        MapRenderer = Map.GetComponent<SpriteRenderer>();
        uIManager = UIManager.Instance;

        StartCoroutine(StartSpawning());
    }


    public void EnemyKilled()
    {
        gameManager.EnemiesKilled++;
        if (gameManager.EnemiesKilled >= CurrentWave.BossSpawnAmount)
        {
            SpawnBoss();
        }
    }

    public void SpawnBoss()
    {
        CurrentWave.Boss.SetActive(true);
    }

    public IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(CurrentWave.spawnRate);
        Enemy randomEnemy = CurrentWave.enemyPrefabs[Random.Range(0, CurrentWave.enemyPrefabs.Count)];
        if(enemyManager.Enemies[randomEnemy.enemyPrefab.name] < CurrentWave.enemyPrefabs[CurrentWave.enemyPrefabs.IndexOf(randomEnemy)].MaxAmount)
        {
            Spawn(randomEnemy.enemyPrefab.name);
        }
        if (Spawning)
        {
            StartCoroutine(StartSpawning());
        }
    }

    void Spawn(string enemy)
    {
        if (Spawning)
        {
            EnemiesAlive++;
            Vector2 spawnPosition = GenerateSpawnPosition();
            GameObject Enemy = objectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GenerateSpawnPosition()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-MapRenderer.size.x, MapRenderer.size.x) / 2, Random.Range(-MapRenderer.size.y, MapRenderer.size.y) / 2);
        Vector2 viewPos = Camera.main.WorldToViewportPoint(spawnPosition);

        while (viewPos.x > 0 && viewPos.x < 1f && viewPos.y > 0 && viewPos.y < 1f)
        {
            spawnPosition = new Vector2(Random.Range(-MapRenderer.size.x, MapRenderer.size.x) / 2, Random.Range(-MapRenderer.size.y, MapRenderer.size.y) / 2);
            viewPos = Camera.main.WorldToViewportPoint(spawnPosition);
        }
        return spawnPosition;
    }

    public void NextWave()
    {
        gameManager.EnemiesKilled = 0;
        uIManager.BossBar.maxValue = CurrentWave.BossSpawnAmount;
    }
}
