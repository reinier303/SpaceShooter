using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Dictionary<string, int> Enemies;

    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Instance = this;
        Enemies = new Dictionary<string, int>();

        enemySpawner = EnemySpawner.Instance;
        foreach (Enemy enemy in enemySpawner.CurrentWave.enemyPrefabs)
        {
            Enemies.Add(enemy.enemyPrefab.name, 0);
        }
    }

    public void NewWave()
    {
        foreach (Enemy enemy in enemySpawner.CurrentWave.enemyPrefabs)
        {
            if(!Enemies.ContainsKey(enemy.enemyPrefab.name))
            {
                Enemies.Add(enemy.enemyPrefab.name, 0);
            }
        }
    }

    // Start is called before the first frame update
    public void AddAmount(string name)
    {
        if(Enemies.ContainsKey(name))
        {
            Enemies[name]++;
        }
    }

    public void RemoveAmount(string name)
    {
        if (Enemies.ContainsKey(name))
        {
            Enemies[name]--;
        }
    }

}
