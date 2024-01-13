using UnityEngine;

public class PlayerCrouchingState : BaseState
{

    public override void EnterState(PlayerStateController manager)
    {
        manager.animationHandler.SwitchState(PlayerAnimationName.CROUCHING);
    }



    public override void UpdateState(PlayerStateController manager)
    {
        if (GameInputManager.Instance.crouchAction.IsPressed() == false)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}