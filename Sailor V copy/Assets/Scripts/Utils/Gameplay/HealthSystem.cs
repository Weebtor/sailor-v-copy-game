using System;

using UnityEngine;

[Serializable]
public class HealthSystem
{
    [field: SerializeField] public int MaxHp { get; private set; } = 1;
    [field: SerializeField] public int CurrentHp { get; private set; }

    public void Reset()
    {
        CurrentHp = MaxHp;
    }
    public void TakeDamage(int damageAmount)
    {
        if (CurrentHp > damageAmount) CurrentHp -= damageAmount;
        else CurrentHp = 0;
    }

}