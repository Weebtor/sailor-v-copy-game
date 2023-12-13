using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int maxHp = 1;
    [SerializeField] int currentHp;
    [SerializeField] int attackPoints = 1;
    protected virtual void Start()
    {
        currentHp = maxHp;
    }


    public void Damage(int damagePoints)
    {
        currentHp -= damagePoints;
        if (currentHp <= 0) OnHpZero();
    }

    protected virtual void OnHpZero()
    {
        Debug.Log("Killbase");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitbox)
    {
        Player player = hitbox.transform.root.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(attackPoints);
            Destroy(gameObject);
        }

    }


}
