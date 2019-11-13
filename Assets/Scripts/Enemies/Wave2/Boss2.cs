using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss2 : AlienShip1
{
    public float FireTime = 1.5f;
    int fireCounter;
    bool secondPhase;
    float SpawnRatePreviousWave;
    public float AttackRandomizerTime = 8;
    public float SplitFireRepeatRate = 4;
    int NextAttackChance;
    int currentAttack;
    int excludedAttack;

    // Start is called before the first frame update
    public override void Start()
    {
        secondPhase = false;
        fireCounter = 0;
        Health = MaxHealth;
        Player = gameManager.Player.transform;
        cameraShake = gameManager.cameraShake;
        NextAttackChance = 101;
        StartCoroutine(AttackRandomizer());
        excludedAttack = 4;
    }

    IEnumerator AttackRandomizer()
    {
        if(Random.Range(0,101) <= NextAttackChance)
        {
            currentAttack = Random.Range(0, 3);
            yield return new WaitForSeconds(2f);
        }
        if(excludedAttack == currentAttack)
        {
            StartCoroutine(AttackRandomizer());
            yield break;
        }
        if (currentAttack == 0 && excludedAttack != 0)
        {
            excludedAttack = 0;
            NextAttackChance = 40;
            StartCoroutine(SplitFire());
        }
        if (currentAttack == 1 && excludedAttack != 1)
        {
            excludedAttack = 1;
            NextAttackChance = 60;
            StartCoroutine(RoundFire());
        }
        if (currentAttack == 2 && excludedAttack != 1)
        {
            excludedAttack = 2;
            StartCoroutine(CornerFire());
            NextAttackChance = 25;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
        //StartCoroutine(CameraChange());
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

    public override void Rotate()
    {

    }

    public IEnumerator StartCoroutineAfterTime(IEnumerator routine, float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(routine);
    }

    IEnumerator SplitFire()
    {
        for (int i = 0; i < 6; i++)
        {
            objectPooler.SpawnFromPool("RedSplitBullet", transform.position, Quaternion.Euler(0, 0, (360 / 6) * i));
        }
        yield return new WaitForSeconds(2f);

        StartCoroutine(AttackRandomizer());
    }

    IEnumerator RoundFire()
    {
        for (int i = 0; i < 90; i++)
        {
            objectPooler.SpawnFromPool("RedEnemyBulletBig", transform.position, Quaternion.Euler(0, 0, (360 / 90) * i));
            yield return new WaitForSeconds(0.06f);
        }
        StartCoroutine(AttackRandomizer());
    }

    IEnumerator CornerFire()
    {
        float offset = 0;
        for(int j = 0; j < 10; j++)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 6; i++)
            {
                objectPooler.SpawnFromPool("RedEnemyBulletSmall", transform.position, Quaternion.Euler(0, 0,((360 / 6) * i) + offset));
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 6; i++)
            {
                objectPooler.SpawnFromPool("RedEnemyBulletSmall", transform.position, Quaternion.Euler(0, 0,30f + ((360 / 6) * i) + offset));
            }
            offset += 10f;
        }
        StartCoroutine(AttackRandomizer());
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
