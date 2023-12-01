using UnityEngine;

public class PlayerCrouchingState : MovementBaseState
{

    public override void EnterState(MovementStateManager manager)
    {
        Debug.Log(PlayerAnimations.CROUCHING);
        manager.animationHandler.SwitchAnimation(PlayerAnimations.CROUCHING);
    }



    public override void UpdateState(MovementStateManager manager)
    {
        if (UserInputScript.instance.CrouchButtonHold == false)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}