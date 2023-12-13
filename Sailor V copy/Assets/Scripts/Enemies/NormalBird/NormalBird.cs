using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D myRb;
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

    protected override void OnHpZero()
    {
        Destroy(gameObject);
    }
}
