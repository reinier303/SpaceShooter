using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CameraShake cameraShake;
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;

    public GameObject Player;
    public int EnemiesKilled;
    public bool BossActive;
    Camera mainCamera;
    public CinemachineVirtualCamera cvCam;

    public bool gamePaused;
    public GameObject PauseMenu;


    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        playerStats = PlayerStats.Instance;
        playerMovement = Player.GetComponent<PlayerMovement>();
        BossActive = false;
        gamePaused = false;
    }

    public void AddUnits(float Units)
    {
        playerStats.Units += Units;
    }

    public IEnumerator MoveCameraToBoss(Transform Boss, float time)
    {
        playerMovement.baseSpeed = 0;
        cvCam.Follow = null;
        StartCoroutine(mainCamera.GetComponent<LerpPosition>().lerpPosition(transform.position, Boss.position, time));
        yield return new WaitForSeconds(time);
        playerMovement.baseSpeed = playerStats.Speed;
        cvCam.Follow = Player.transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
