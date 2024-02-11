using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat1 : BatEnemy
{
    // Start is called before the first frame update
    [Header("Componets")]
    [SerializeField] Rigidbody2D myRb;

    [Header("Movement Settings")]
    [SerializeField] float xSpeed = 4f;

    protected override void Start()
    {
        base.Start();
        myRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckPosition();
        Move();

    }
    void Move()
    {
        myRb.velocity = transform.right * xSpeed;
    }
}
