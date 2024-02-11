using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BatEnemy : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    [Header("Stats Settings")]
    [SerializeField] HealthSystem health;
    [SerializeField] StatsSystem stats;

    [field: Header("Game values")]
    [field: SerializeField] public int ScorePoints { get; private set; } = 1;

    [Header("Events")]
    [SerializeField] GameEvent OnEnemyOffScreen;
    [SerializeField] GameEvent OnEnemyDestruction;
    [SerializeField] GameEvent OnEnemyDefeated;

    protected virtual void Start()
    {
        health.Reset();
    }


    public void TakeDamage(int damageAmount)
    {
        health.TakeDamage(damageAmount);

        if (health.CurrentHp <= 0)
            OnHealthDepleted();
    }
    void OnHealthDepleted()
    {
        OnEnemyDefeated.Raise(this, null);
        HandleEnemyDestruction();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthController targetPlayer = other.GetComponentInParent<PlayerHealthController>();
        if (targetPlayer == null) return;

        targetPlayer.TakeDamage(stats.AttackPoints);
        HandleEnemyDestruction();
    }

    protected void HandleEnemyDestruction()
    {
        OnEnemyDestruction.Raise(this);
        Destroy(gameObject);
    }

    void IDamageable.OnHealthDepleted()
    {
        throw new System.NotImplementedException();
    }
    protected void CheckPosition()
    {
        if (transform.position.x <= -2.7)
        {
            Debug.Log("Event raised");
            OnEnemyOffScreen.Raise(this);
            Destroy(gameObject);
        }
    }

}
