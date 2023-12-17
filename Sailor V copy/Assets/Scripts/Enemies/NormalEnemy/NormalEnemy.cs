using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyBase
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
        InitialMovement();
    }

    void InitialMovement()
    {
        myRb.velocity = transform.right * xSpeed;
    }


}
