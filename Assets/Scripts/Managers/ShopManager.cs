using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ShopManager : MonoBehaviour
{
    public Text UnitsText;
    PlayerStats playerStats;
    public Dictionary<string, bool> ShopItems;
    public GameObject LoadingScreen;
    public Slider LoadingBar;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UnitsText.text = "Units: " + playerStats.TotalUnits;
    }

    public void StartGame()
    {
        SaveSystem.SavePlayer(playerStats);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(2f);

        AsyncOperation level = SceneManager.LoadSceneAsync(1);

        while (level.progress < 1)
        {
            LoadingBar.value = level.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
