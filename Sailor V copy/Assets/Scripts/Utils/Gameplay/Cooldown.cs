


using UnityEngine;

[System.Serializable]
public class CoolDown
{
    [SerializeField] float cooldownTime = 1f;
    float nextCooldown;
    public bool IsCoolingDown => Time.time < nextCooldown;
    public void StartCooldown() => nextCooldown = Time.time + cooldownTime;
}