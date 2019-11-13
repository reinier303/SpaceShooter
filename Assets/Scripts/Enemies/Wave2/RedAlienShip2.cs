using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlienShip2 : RedAlienShip1
{
    public float BulletTurnTime;
    GameObject bullet1;
    GameObject bullet2;

    public override IEnumerator Fire()
    {
        if(Vector2.Distance(Player.position, transform.position) <= FireDistance)
        {
            StartCoroutine(WaitAfterFiring());

            bullet1 = objectPooler.SpawnFromPool("RedEnemyBullet", transform.position + transform.up * 0.5f, Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + 45));
            bullet2 = objectPooler.SpawnFromPool("RedEnemyBullet", transform.position + transform.up * 0.5f, Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + -45));
        }
        StartCoroutine(ChangeBulletDirection());
        yield return new WaitForSeconds(FireTime);
        StartCoroutine(Fire());
    }

    IEnumerator ChangeBulletDirection()
    {
        yield return new WaitForSeconds(BulletTurnTime);
        if(bullet1 != null && bullet2 != null)
        {
            bullet1.transform.Rotate(0, 0, -90);
            bullet2.transform.Rotate(0, 0, 90);
            bullet1 = null;
            bullet2 = null;
        }
    }
}
