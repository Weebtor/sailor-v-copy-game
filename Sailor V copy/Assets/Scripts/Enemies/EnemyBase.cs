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

    [field: Header("Game values")]
    [field: SerializeField] public int scorePoints { get; private set; } = 1;

    [Header("Events")]
    public GameEvent OnEnemyDestruction;
    public GameEvent OnEnemyDefeated;

    protected virtual void Start()
    {
        currentHp = MAX_HP;
    }


    public void TakeDamage(int damagePoints)
    {
        currentHp -= damagePoints;
        if (currentHp <= 0)
            Deafeat();
    }

    protected virtual void Deafeat()
    {
        OnEnemyDefeated.Raise(this, null);
        HandleEnemyDestruction();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Player player = other.GetComponentInParent<Player>();

        if (player == null) return;

        player.TakeDamage(attackPoints);
        HandleEnemyDestruction();

    }

    protected void HandleEnemyDestruction()
    {
        OnEnemyDestruction.Raise(this, null);
        Destroy(gameObject);
    }


}
