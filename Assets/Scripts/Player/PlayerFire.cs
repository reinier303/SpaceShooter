using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !gameManager.gamePaused)
        {
            weapon.Fire();
        }
    }
}
