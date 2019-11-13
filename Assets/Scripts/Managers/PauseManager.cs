using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void Continue()
    {
        gameManager.PauseGame();
        gameObject.SetActive(false);
    }
    public void LoadScene(int scene)
    {
        Time.timeScale = 1;
        SaveSystem.SavePlayer(PlayerStats.Instance);
        SceneManager.LoadScene(scene);
    }
}
