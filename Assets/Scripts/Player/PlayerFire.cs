using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    PlayerStats playerStats;
    ObjectPooler objectPooler;
    AudioManager audioManager;
    GameManager gameManager;

    float FireRate;
    private bool FireCooldown;
    public float extraRotation = 1f;
    int projectileCount;
    bool multipleProjectiles;
    public ParticleSystem Muzzle;
    

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        playerStats = PlayerStats.Instance;
        audioManager = AudioManager.Instance;
        gameManager = GameManager.Instance;
        FireRate = playerStats.FireRate;
        multipleProjectiles = playerStats.MultipleProjectiles;
        projectileCount = (int)playerStats.ProjectileCount;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !gameManager.gamePaused)
        {
            Fire();
        }
    }
    void Fire()
    {
        if (!FireCooldown)
        {
            Muzzle.Play();
            audioManager.Play("Shot");
            StartCoroutine(StartFireCooldown());

            int direction = 1;
            for (int i = 0;i < projectileCount; i++)
            {
                objectPooler.SpawnFromPool("Bullet", transform.position + (transform.up / 3), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + (i * extraRotation * direction)));
                direction *= -1;
            }
            
        }
    }

    IEnumerator StartFireCooldown()
    {
        FireCooldown = true;
        yield return new WaitForSeconds(1 / FireRate);
        FireCooldown = false;
    }
}
