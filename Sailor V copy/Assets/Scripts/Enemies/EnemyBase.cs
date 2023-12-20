using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Stats Settings")]
    [SerializeField] const int MAX_HP = 1;
    [System.NonSerialized] int currentHp;
    [SerializeField] int attackPoints = 1;
    [Header("Game stats")]
    public readonly int scorePoints = 1;

    [Header("Events")]
    public GameEvent OnEnemyDestruction;

    protected virtual void Start()
    {
        currentHp = MAX_HP;
    }


    public void Damage(int damagePoints)
    {
        currentHp -= damagePoints;
        if (currentHp <= 0) OnTakedown();
    }

    protected virtual void OnTakedown()
    {
        Debug.Log("Takedown base");
        HandleEnemyDestruction();

    }

    void OnTriggerEnter2D(Collider2D hitbox)
    {
        Player player = hitbox.transform.root.GetComponent<Player>();
        if (player)
        {
            Debug.Log($"<color=red>{attackPoints}</color>");
            player.TakeDamage(attackPoints);
            HandleEnemyDestruction();
        }

    }

    protected void HandleEnemyDestruction()
    {
        OnEnemyDestruction.Raise(this, null);
        Destroy(gameObject);
    }


}
