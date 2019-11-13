using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReceiveDamage : MonoBehaviour
{
    public static PlayerReceiveDamage Instance;
    Animator animator;
    PlayerStats playerStats;
    AudioManager audioManager;
    public float HitCooldown = 1.5f;
    public bool CanTakeDamage;

    public Animator Vignette;

    private void Awake()
    {
        Instance = this;
        playerStats = PlayerStats.Instance;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
        CanTakeDamage = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartTakeDamage();
        }
        if(playerStats.Health <= 0)
        {
            Die();
        }
    }

    public void StartTakeDamage()
    {
        StartCoroutine(TakeDamage());
    }

    public IEnumerator TakeDamage()
    {       
        if(CanTakeDamage)
        {
            CanTakeDamage = false;
            playerStats.Health--;
            audioManager.Play("PlayerExplosion");
            Vignette.SetTrigger("Hit");
            animator.SetTrigger("Hit");
            yield return new WaitForSeconds(HitCooldown);
            CanTakeDamage = true;
        }
    }

    void Die()
    {
        PlayerStats.Instance.AddToTotal();
        SaveSystem.SavePlayer(PlayerStats.Instance);
        SceneManager.LoadScene(2);
    }
}
