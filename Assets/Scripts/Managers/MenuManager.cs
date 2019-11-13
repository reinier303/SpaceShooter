using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject LoadingScreen;
    public Slider LoadingBar;

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame(int scene)
    {
        StartCoroutine(LoadGame(scene));
    }

    IEnumerator LoadGame(int scene)
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        AsyncOperation level = SceneManager.LoadSceneAsync(scene);

        while (level.progress < 1)
        {
            LoadingBar.value = level.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
