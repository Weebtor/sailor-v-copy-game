using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : EnemyBase
{
    // Start is called before the first frame update
    public Rigidbody2D myRb;
    public float xSpeed = 4f;
    protected override void Start()
    {
        base.Start();
        myRb.velocity = transform.right * xSpeed;
    }

    void Update()
    {
    }
    // Update is called once per frame
    public override void Move()
    {
        // here goes como the enemy must move and another things
        base.Move();
    }

    protected override void OnHpZero()
    {
        // Debug.Log("KIll this Object");
        Destroy(gameObject);
    }
}
