using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchingState : BaseState
{
    InputAction CrouchAction => GameInputManager.Instance.PlayerInputs.actions["Crouch"];

    public override void EnterState(PlayerStateController manager)
    {
        manager.animationController.SwitchState(PlayerAnimationName.CROUCHING);
    }



    public override void UpdateState(PlayerStateController manager)
    {
        if (CrouchAction.IsPressed() == false)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}