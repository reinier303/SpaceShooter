using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 3f;
    public float BoostSpeed = 1.5f;
    private Vector3 target;
    private Vector3 position;
    private Camera cam;
    [SerializeField]
    bool BoostActive;
    bool Stay;
    public float baseSpeed;
    public SpriteRenderer Map;
    public ParticleSystem RegularSystem;
    public ParticleSystem BoostSystem;
    PlayerStats playerStats;
    GameManager gameManager;
    PlayerReceiveDamage playerReceiveDamage;
    bool boostUnlocked;
    public GameObject CenterText;
    bool DamagingPlayer;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        playerStats = PlayerStats.Instance;
        playerReceiveDamage = PlayerReceiveDamage.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        BoostSystem.Stop();
        RegularSystem.Play();
        boostUnlocked = playerStats.BoostUnlocked;
        Speed = playerStats.Speed;
        position = gameObject.transform.position;

        cam = Camera.main;

        baseSpeed = Speed;

        DamagingPlayer = false;
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // compute where the mouse is in world space
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        target = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target) > 0.5f && !gameManager.gamePaused)
        {
            Move();
            Rotate();
        }
        if (transform.position.x > Map.size.x / 2 || -transform.position.x < -Map.size.x / 2 || transform.position.y > Map.size.y / 2 || -transform.position.y < -Map.size.y / 2)
        {
            if(!DamagingPlayer)
            {
                StartCoroutine(DamagePlayer());
            }
        }
        else
        {           
            if(CenterText.activeSelf)
            {
                CenterText.SetActive(false);
            }
        }
        //Stay In Place
        if (Input.GetMouseButtonDown(1))
        {
            Stay = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Stay = false;
        }
        StayInPlace();

        //Boost
        if (Input.GetButtonDown("Jump"))
        {
            BoostActive = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            BoostActive = false;
        }
        if (boostUnlocked)
        {
            Boost();
        }
    }

    IEnumerator DamagePlayer()
    {
        DamagingPlayer = true;
        CenterText.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (transform.position.x > Map.size.x / 2 || -transform.position.x < -Map.size.x / 2 || transform.position.y > Map.size.y / 2 || -transform.position.y < -Map.size.y / 2)
        {
            playerReceiveDamage.StartTakeDamage();
        }
        else
        {
            CenterText.SetActive(false);
        }
        DamagingPlayer = false;
    }

    private void Move()
    {
        float step = Speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void Rotate()
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Boost()
    {
        if(BoostActive)
        {
            if(!BoostSystem.isPlaying && !Stay)
            {
                BoostSystem.Play();
                RegularSystem.Stop();
            }
            if(!Stay)
            {
                Speed = baseSpeed + BoostSpeed;
            }
        }
        if (!BoostActive)
        {
            if (!RegularSystem.isPlaying && !Stay)
            {
                BoostSystem.Stop();
                RegularSystem.Play();
                Speed = baseSpeed;
            }          
        }
    }

    void StayInPlace()
    {
        if (Stay)
        {
            Speed = 0;
            if (RegularSystem.isPlaying || BoostSystem.isPlaying)
            {
                RegularSystem.Stop();
                BoostSystem.Stop();
            }
        }
        if (!Stay)
        {
            Speed = baseSpeed;
            if (!RegularSystem.isPlaying && !BoostActive)
            {
                RegularSystem.Play();
            }
        }
    }
}
