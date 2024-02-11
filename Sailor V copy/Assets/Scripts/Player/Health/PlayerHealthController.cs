using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [Header("Events")]
    public GameEvent OnPlayerHealthChanged;

    [Header("Player Health")]
    [SerializeField] HealthSystem Health;

    PlayerStateController stateManager;
    PlayerAnimationController animationHandler;

    public int GetMaxHealth() => Health.MaxHp;
    public int GetCurrentHealth() => Health.CurrentHp;

    void Awake()
    {
        Health.Reset();
    }
    void Start()
    {
        stateManager = gameObject.GetComponentInChildren<PlayerStateController>();
        animationHandler = gameObject.GetComponentInChildren<PlayerAnimationController>();
    }

    public void TakeDamage(int damageAmount = 1)
    {
        Health.TakeDamage(damageAmount);
        OnPlayerHealthChanged.Raise(this);

        if (Health.CurrentHp > 0)
            TakeDamageAnimation();
        else
            OnHealthDepleted();
    }

    public void TakeDamageAnimation()
    {
        animationHandler.TakeDamageAnimation();
    }

    public void OnHealthDepleted()
    {
        stateManager.SwitchState(stateManager.DeadState);
    }

    public void OnListenerEnemyOffScreen(Component sender, object data)
    {
        Debug.Log("Event listened");
        Health.TakeDamage(1);
        OnPlayerHealthChanged.Raise(this);

        if (Health.CurrentHp == 0)
            OnHealthDepleted();
    }
}
