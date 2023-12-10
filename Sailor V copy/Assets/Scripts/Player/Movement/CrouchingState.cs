using UnityEngine;

public class PlayerCrouchingState : MovementBaseState
{

    public override void EnterState(MovementStateManager manager)
    {
        manager.animationHandler.SwitchState(PlayerAnimation.CROUCHING);
    }



    public override void UpdateState(MovementStateManager manager)
    {
        if (UserInputScript.instance.CrouchButtonHold == false)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}