using UnityEngine;

public class PlayerIdleState : BaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    ContactFilter2D groundFilter;
    public override void EnterState(PlayerStateController manager)
    {
        rigidbody = manager.myRb;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        groundCollider = manager.groundCollider;
        HandleGroundSnap(manager);

        manager.animationHandler.SwitchState(PlayerAnimationName.IDLE);
    }
    public override void UpdateState(PlayerStateController manager)
    {

        if (GameInputManager.Instance.jumpAction.IsPressed())
        {
            manager.SwitchState(manager.JumpingState);
        }
        else if (GameInputManager.Instance.crouchAction.IsPressed())
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
            Vector2 surfacePosition = Physics2D.ClosestPoint(manager.transform.position + new Vector3(0, 1), groundCastBuffer[0].collider);
            rigidbody.transform.position = surfacePosition;
        }
    }

}