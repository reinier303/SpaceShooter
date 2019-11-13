using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlienShip1 : AlienShip1
{
    public float FireTime = 1.5f;
    public float WaitAfterFireTime;
    public float FireDistance;
    float baseSpeed;

    public override void Start()
    {
        base.Start();
        baseSpeed = Speed;
        StartCoroutine(StartCoroutineAfterTime(Fire(), 5f));
    }

    public IEnumerator StartCoroutineAfterTime(IEnumerator routine, float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(routine);
    }

    public virtual IEnumerator Fire()
    {
        if (Vector2.Distance(Player.position, transform.position) <= FireDistance)
        {
            StartCoroutine(WaitAfterFiring());

            objectPooler.SpawnFromPool("RedEnemyBullet", transform.position + transform.up * 0.5f, transform.rotation);                    
        }
        yield return new WaitForSeconds(FireTime);
        StartCoroutine(Fire());
    }

    public IEnumerator WaitAfterFiring()
    {
        Speed = 0;
        yield return new WaitForSeconds(WaitAfterFireTime);
        Speed = baseSpeed;
    }
}
