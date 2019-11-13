using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{
    public string Name;
    [Header("Enemy Prefabs")]  
    public List<Enemy> enemyPrefabs;
    public float spawnRate;
    public int BossSpawnAmount;
    public GameObject Boss;
    public int WaveNumber;

}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int MaxAmount;
}
