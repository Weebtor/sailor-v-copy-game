using System;
using UnityEngine;

public class RedBird : EnemyBase
{

    [SerializeField] float xSpeed = 1f;

    [SerializeField] float yAmplitude = 1f;
    [SerializeField] float frequency = 1f;
    [SerializeField] float fase = 1f;


    private Rigidbody2D myRb;

    protected override void Start()
    {
        base.Start();
        myRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float verticalSpeed = transform.up.y * (float)(yAmplitude * Math.Sin(2 + Math.PI * frequency * Time.time + fase));
        float horizontalSpeed = transform.right.x * xSpeed;
        myRb.velocity = new Vector2(horizontalSpeed, verticalSpeed);

    }
}
