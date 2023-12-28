using UnityEngine;

public class PlayerCrouchingState : BaseState
{

    public override void EnterState(PlayerStateManager manager)
    {
        manager.animationHandler.SwitchState(PlayerAnimationName.CROUCHING);
    }



    public override void UpdateState(PlayerStateManager manager)
    {
        if (GameInputManager.Instance.crouchAction.IsPressed() == false)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}