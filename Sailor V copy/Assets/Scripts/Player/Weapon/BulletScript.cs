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
    [SerializeField] int damage = 5;

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;
        if (otherLayer == (int)General.Layers.Hurtbox)
        {
            EnemyBase enemy = other.transform.root.GetComponent<EnemyBase>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
                HandleDestroy();
            }
        }
    }

    void HandleDestroy()
    {
        // for object pulling in the future
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
