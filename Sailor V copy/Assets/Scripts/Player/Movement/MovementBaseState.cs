using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void EnterState(MovementStateManager manager);
    public abstract void UpdateState(MovementStateManager manager);

}