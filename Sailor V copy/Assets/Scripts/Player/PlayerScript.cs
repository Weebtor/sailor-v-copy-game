using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] float speed = 1;
    [SerializeField] int maxHp = 10;
    [SerializeField] int currentHp;
    void Start()
    {
        currentHp = maxHp;
    }
    public void Damage(int damagePoints)
    {
        currentHp -= damagePoints;
        if (currentHp <= 0) OnHpZero();
    }

    void OnHpZero()
    {
        MovementStateManager stateManager = gameObject.GetComponentInChildren<MovementStateManager>();
        stateManager.SwitchState(stateManager.DeadState);
    }

    // debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.02f);

    }

}
