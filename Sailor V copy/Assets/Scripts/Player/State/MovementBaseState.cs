using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(PlayerStateManager manager);
    public abstract void UpdateState(PlayerStateManager manager);
    // public abstract void ExitState(MovementStateManager manager);

}