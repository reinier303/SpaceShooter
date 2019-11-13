using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : BaseEnemy
{
    private float randomRotation;
    private Rigidbody2D rb;

    [Header("Meteor Force and Torque")]
    public float MaxThrust;
    public float MaxTorque;
    public float MinSize;
    public float MaxSize;

    public override void Start()
    {
        base.Start();
        //Set random size
        Vector2 randomSize = new Vector2(Random.Range(MinSize, MaxSize), Random.Range(MinSize, MaxSize));

        transform.localScale = randomSize;

        //Get rigidbody and add torque and force;
        rb = GetComponent<Rigidbody2D>();

        Vector2 thrust = new Vector2(Random.Range(-MaxThrust, MaxThrust), Random.Range(-MaxThrust, MaxThrust));
        float torque = Random.Range(-MaxTorque, MaxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

    }

    public void Initialize()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;

        Vector2 thrust = new Vector2(Random.Range(-MaxThrust, MaxThrust), Random.Range(-MaxThrust, MaxThrust));
        float torque = Random.Range(-MaxTorque, MaxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }
    
}
