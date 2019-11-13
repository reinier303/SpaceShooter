using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : Bullet
{
    public PlayerReceiveDamage playerReveiveDamage;
    public override void Start()
    {
        base.Start();
        playerReveiveDamage = PlayerReceiveDamage.Instance;
        StartCoroutine(DisableAfterTime());
    }

    public override IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(AliveTime);
        gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerReveiveDamage.CanTakeDamage)
        {
            other.GetComponent<PlayerReceiveDamage>().StartTakeDamage();
            gameObject.SetActive(false);
        }
    }
}
