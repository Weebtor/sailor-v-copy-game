using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    [SerializeField] int currentHp;

    PlayerStateManager stateManager;
    PlayerAnimationController animationHandler;

    void Start()
    {
        currentHp = maxHp;
        stateManager = gameObject.GetComponentInChildren<PlayerStateManager>();
        animationHandler = gameObject.GetComponentInChildren<PlayerAnimationController>();
    }


    [Header("Events")]
    public GameEvent OnPlayerTakeDamage;

    public void TakeDamage(int value = 1)
    {
        if (currentHp <= 0)
            return;

        currentHp -= value;
        OnPlayerTakeDamage.Raise(this, value);
        if (currentHp <= 0)
            OnHpZero();
        else
            animationHandler.TakeDamageAnimation();
    }

    void OnHpZero()
    {
        stateManager.SwitchState(stateManager.DeadState);
    }

}