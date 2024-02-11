using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;

    public Sound sfx_shoot;

    [Header("Stats settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] StatsSystem stats;

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        int parentLayer = other.transform.parent.gameObject.layer;
        if (parentLayer == (int)General.Layers.Hurtbox)
        {
            BatEnemy enemy = other.transform.root.GetComponent<BatEnemy>();
            if (enemy)
            {
                enemy.TakeDamage(stats.AttackPoints);
                HandleDestroy();
            }
        }
    }

    void HandleDestroy()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
