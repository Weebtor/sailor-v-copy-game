using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    ContactFilter2D groundFilter;

    InputAction JumpAction => GameInputManager.Instance.PlayerInputs.actions["Jump"];
    InputAction CrouchAction => GameInputManager.Instance.PlayerInputs.actions["Crouch"];

    public override void EnterState(PlayerStateController manager)
    {
        rigidbody = manager.myRb;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        groundCollider = manager.groundCollider;
        HandleGroundSnap(manager);

        manager.animationController.SwitchState(PlayerAnimationName.IDLE);
    }
    public override void UpdateState(PlayerStateController manager)
    {

        if (JumpAction.IsPressed())
        {
            manager.SwitchState(manager.JumpingState);
        }
        else if (CrouchAction.IsPressed())
        {
            manager.SwitchState(manager.CrouchingState);
        }

    }

    void HandleGroundSnap(PlayerStateController manager)
    {
        RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];
        int hit = groundCollider.Cast(Vector2.down, groundFilter, groundCastBuffer, 0f);
        if (hit > 0)
        {
            Vector2 surfacePosition = Physics2D.ClosestPoint(manager.transform.position + Vector3.up, groundCastBuffer[0].collider);
            rigidbody.transform.position = surfacePosition;
        }
    }

}