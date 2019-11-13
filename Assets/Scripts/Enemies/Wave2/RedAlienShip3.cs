using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlienShip3 : RedAlienShip1
{
    public float BetweenBulletTime = 0.1f;

    public override IEnumerator Fire()
    {

        if(Vector2.Distance(Player.position, transform.position) <= FireDistance)
        {
            StartCoroutine(WaitAfterFiring());
            for (int i = 0; i < 3; i ++)
            {
                objectPooler.SpawnFromPool("RedEnemyBulletSmall", transform.position + transform.up * 0.5f, transform.rotation);
                yield return new WaitForSeconds(BetweenBulletTime);
            }
        }
        yield return new WaitForSeconds(FireTime);
        StartCoroutine(Fire());
    }

}
