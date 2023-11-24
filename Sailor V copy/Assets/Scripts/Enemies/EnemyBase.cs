using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed = 1;
    [SerializeField] int maxHp = 10;
    [SerializeField] int currentHp;
    [SerializeField] int attackPoints = 1;
    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public EnemyBase()
    {
        currentHp = maxHp;
    }

    public virtual void Move() { }
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
        Debug.Log(hitbox.name);
        var player = hitbox.transform.root.GetComponent<PlayerScript>();
        if (player)
        {
            Debug.Log("inflict damage");
            player.Damage(attackPoints);
        }
        Destroy(gameObject);

    }


}
