using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShip1 : BaseEnemy
{
    public Transform Player;
    public float Speed = 2;
    public float StopDistance = 0.5f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Player = gameManager.Player.transform;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Move();
        Rotate();
    }

    public virtual void Move()
    {
        if(Vector2.Distance(transform.position, Player.transform.position) > StopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        }
    }

    public virtual void Rotate()
    {
        Vector3 difference = Player.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
    }
}
