using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSplitBullet : AlienBullet
{
    public float SplitCount;

    public override IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(AliveTime);
        Split();
        //gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerReceiveDamage>().StartTakeDamage();
            Split();
            gameObject.SetActive(false);
        }
    }

    void Split()
    {
        for(int i = 0; i < SplitCount; i ++)
        {
            objectPooler.SpawnFromPool("RedEnemyBullet", transform.position, Quaternion.Euler(0,0,(360 / SplitCount) * i));
        }
    }
}
