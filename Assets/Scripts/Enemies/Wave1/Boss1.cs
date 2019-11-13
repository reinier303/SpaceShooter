using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss1 : AlienShip1
{
    public float TinySpawnTime = 5;
    public float FireTime = 1.5f;
    int fireCounter;
    bool secondPhase;
    public Material SecondPhaseMaterial;
    float SpawnRatePreviousWave;

    // Start is called before the first frame update
    public override void Start()
    {
        secondPhase = false;
        fireCounter = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = MaxHealth;
        Player = gameManager.Player.transform;
        cameraShake = gameManager.cameraShake;
        StartCoroutine(StartCoroutineAfterTime(SpawnTiny(), 5f));
        StartCoroutine(StartCoroutineAfterTime(Fire(), 5f));
    }

    public override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
        StartCoroutine(CameraChange());
        gameManager.BossActive = true;
        //StartCoroutine(gameManager.MoveCameraToBoss(transform, 2f));
        enemySpawner = EnemySpawner.Instance;
        SpawnRatePreviousWave = enemySpawner.CurrentWave.spawnRate;
        enemySpawner.CurrentWave.spawnRate = 5;
    }

    IEnumerator CameraChange()
    {
        animator.SetBool("Camera", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Camera", false);
    }

    public override void Update()
    {
        base.Update();
        if(Health <= MaxHealth / 2 && secondPhase != true)
        {
            animator.SetBool("SecondPhase", true);
            FireTime = (FireTime / 3) * 2;
            secondPhase = true;
        }
    }

    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0,0), Speed * Time.deltaTime);
    }

    public IEnumerator StartCoroutineAfterTime(IEnumerator routine, float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(routine);
    }

    IEnumerator SpawnTiny()
    {
        objectPooler.SpawnFromPool("TinyAlienShip1", transform.position + transform.up, transform.rotation);
        yield return new WaitForSeconds(TinySpawnTime);
        StartCoroutine(SpawnTiny());
    }

    IEnumerator Fire()
    {
        fireCounter++;
        objectPooler.SpawnFromPool("EnemyBullet", transform.position + transform.up * 1.5f, transform.rotation);
        if(fireCounter >= 3)
        {
            SideFire();
            fireCounter = 0;
        }
        yield return new WaitForSeconds(FireTime);
        StartCoroutine(Fire());
    }

    void SideFire()
    {
        objectPooler.SpawnFromPool("EnemyBullet", transform.position + transform.up * 1.5f, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 15));
        objectPooler.SpawnFromPool("EnemyBullet", transform.position + transform.up * 1.5f, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 15));
        if(secondPhase)
        {
            objectPooler.SpawnFromPool("EnemyBullet", transform.position + transform.up * 1.5f, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 30));
            objectPooler.SpawnFromPool("EnemyBullet", transform.position + transform.up * 1.5f, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 30));
        }
    }

    public override void Die()
    {
        gameManager.BossActive = false;
        if(enemySpawner.CurrentWave.WaveNumber < enemySpawner.Waves.Length)
        {
            enemySpawner.CurrentWave = enemySpawner.Waves[enemySpawner.CurrentWave.WaveNumber];
            enemyManager.NewWave();
            enemySpawner.NextWave();

        }
        else
        {
            enemySpawner.CurrentWave.spawnRate = SpawnRatePreviousWave;
        }
        DropCoins();
        GameObject DeathEffect = objectPooler.SpawnFromPool(DeathEffectName, transform.position, Quaternion.identity);
        DeathEffect.transform.localScale = new Vector2(Mathf.Max(transform.localScale.x, transform.localScale.y) * 5f * DeathEffect.transform.localScale.x, Mathf.Max(transform.localScale.x, transform.localScale.y) * 5f * DeathEffect.transform.localScale.y);
        cameraShake.StartCoroutine(cameraShake.Shake(2.8f, ShakeMagnitude));
        gameObject.SetActive(false);
    }
}
