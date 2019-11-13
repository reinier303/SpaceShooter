using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    private GameManager gameManager;
    public float value;
    private GameObject player;
    public float disableTime;
    AudioSource audioSource;
    Animator animator;
    ObjectPooler objectPooler;
    CircleCollider2D collider;
    Camera camera;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        player = gameManager.Player;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CircleCollider2D>();
        camera = Camera.main;
    }

    private void OnEnable()
    {
        collider.enabled = true;
        StartCoroutine(DisableAfterTime());
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 1.5f);
        }
    }

    public void ChangePosition()
    {
        StartCoroutine(lerpPosition(transform.position, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), 0.35f));
    }

    IEnumerator lerpPosition(Vector3 StartPos, Vector3 EndPos, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            transform.position = Vector3.Lerp(StartPos, EndPos, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            collider.enabled = false;
            objectPooler.SpawnFromPool("CoinEffect", transform.position, Quaternion.identity);
            StartCoroutine(MoveToUnits());
        }
    }

    IEnumerator MoveToUnits()
    {
        float time = 0.5f;
        Vector2 movePosition = new Vector2(camera.transform.position.x - 10, camera.transform.position.y + 6.4f);
        StartCoroutine(lerpPosition(transform.position, movePosition, time));
        yield return new WaitForSeconds(time);
        gameManager.AddUnits(value);
        gameObject.SetActive(false);
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(disableTime - 2);
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
