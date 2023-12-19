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
    // void Start()
    // {
    //     // sfx_shoot.RegisterAudio(this.gameObject);
    // }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D hit)
    {
        var enemy = hit.transform.root.GetComponent<EnemyBase>();
        if (enemy)
        {
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
