using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public float UnitsGiven;
    public GameManager gameManager;
    public ObjectPooler objectPooler;
    public EnemyManager enemyManager;
    public EnemySpawner enemySpawner;
    public AudioManager audioManager;
    public Animator animator;

    //DeathEffects
    public CameraShake cameraShake;
    [Header("Death Effect properties")]
    public string DeathEffectName;
    public float ShakeMagnitude = 1.8f;
    public SpriteRenderer spriteRenderer;
    bool pooled;
    public float EffectScaleFactor;

    public virtual void Awake()
    {
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        enemyManager = EnemyManager.Instance;
        enemySpawner = EnemySpawner.Instance;
        audioManager = AudioManager.Instance;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.Log("No animator found");
        }
        pooled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = gameManager.cameraShake;
        Health = MaxHealth;
        InvokeRepeating("BoundsCheck", 0f, 0.5f);
    }

    public virtual void OnEnable()
    {
        Health = MaxHealth;
        if(!pooled)
        {
            pooled = true;
        }
        else
        {
            string name = gameObject.name.Replace("(Clone)", "");
            enemyManager.AddAmount(name);
        }
    }


    public void RemoveFromAmount()
    {      
        string name = gameObject.name.Replace("(Clone)", "");
        enemyManager.RemoveAmount(name);
    }

    public virtual void Update()
    {
        if(Health <= 0)
        {
            Die();
        }
    }
    
    void BoundsCheck()
    {
        if (transform.position.y > 25 + transform.localScale.y ||
            transform.position.y < -25 - transform.localScale.y ||
            transform.position.x > 25 + transform.localScale.x ||
            transform.position.x < -25 - transform.localScale.x)
        {           
            gameObject.SetActive(false);
            if(gameObject.activeSelf)
            {
                RemoveFromAmount();
            }
        }
    }

    public virtual void Die()
    {
        DropCoins();
        enemySpawner.EnemyKilled();
        string name = gameObject.name.Replace("(Clone)", "");
        RemoveFromAmount();
        GameObject DeathEffect = objectPooler.SpawnFromPool(DeathEffectName, transform.position, Quaternion.identity);
        cameraShake.StartCoroutine(cameraShake.Shake(0.4f, ShakeMagnitude));
        gameObject.SetActive(false);
    }

    public void DropCoins()
    {
        int coinAmountLarge = (int)((UnitsGiven - UnitsGiven % 50) / 50);
        int coinAmountMedium = (int)(((UnitsGiven - UnitsGiven % 5) / 5) - (coinAmountLarge * 10));
        int coinAmountSmall = (int)((UnitsGiven / 0.5f) - (coinAmountLarge * 100) - (coinAmountMedium * 10));
        for (int i = 0; i < coinAmountLarge; i++)
        {
            GameObject coin = objectPooler.SpawnFromPool("CoinLarge", transform.position, Quaternion.Euler(0, 0, Random.Range(-360, 360)));
            coin.GetComponent<Units>().ChangePosition();
        }
        for (int i = 0; i < coinAmountMedium; i++)
        {
            GameObject coin = objectPooler.SpawnFromPool("CoinMedium", transform.position, Quaternion.Euler(0,0, Random.Range(-360,360)));
            coin.GetComponent<Units>().ChangePosition();
        }
        for (int i = 0; i < coinAmountSmall; i++)
        {
            GameObject coin = objectPooler.SpawnFromPool("CoinSmall", transform.position, Quaternion.Euler(0, 0, Random.Range(-360, 360)));
            coin.GetComponent<Units>().ChangePosition();
        }
    }

    public void PlayHitAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger("Hit");
        }
    }
}
