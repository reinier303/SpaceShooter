using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float AliveTime;
    public float Damage;
    public ObjectPooler objectPooler;
    public GameManager gameManager;

    public virtual void Start()
    {
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(DisableAfterTime());
        Damage = PlayerStats.Instance.Damage;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }

    public virtual IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(AliveTime);
        objectPooler.SpawnFromPool("BulletPop2", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            objectPooler.SpawnFromPool("BulletPop2", transform.position, Quaternion.identity);
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            enemy.Health -= Damage;
            enemy.PlayHitAnimation();

            gameObject.SetActive(false);
        }
    }
}
