using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    PlayerStats playerStats;

    public Text UnitsText;
    public Text LivesText;
    public Slider BossBar;
    private EnemySpawner enemySpawner;
    private GameManager gameManager;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerStats = PlayerStats.Instance;
        enemySpawner = EnemySpawner.Instance;
        BossBar.maxValue = enemySpawner.CurrentWave.BossSpawnAmount;
        BossBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UnitsText.text = "Units: " + Mathf.Round(playerStats.Units * 10) / 10;
        LivesText.text = "Lives: " + playerStats.Health;
        if(!gameManager.BossActive)
        {
            BossBar.value = gameManager.EnemiesKilled;
        }
    }
}
