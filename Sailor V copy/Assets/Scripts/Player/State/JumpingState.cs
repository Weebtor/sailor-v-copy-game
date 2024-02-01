using UnityEngine;

public class PlayerJumpingState : BaseState
{
    Rigidbody2D rigidbody;
    bool IsFalling => rigidbody.velocity.y <= 0;

    public override void EnterState(PlayerStateController manager)
    {
        rigidbody = manager.myRb;
        float initialVerticalVelocity = Mathf.Sqrt(manager.JumpHeight * -2 * Physics2D.gravity.y * manager.GravityScale);
        rigidbody.velocity = new Vector2(rigidbody.velocity.y, initialVerticalVelocity);

        manager.animationHandler.SwitchState(PlayerAnimationName.JUMPING);
    }
    public override void UpdateState(PlayerStateController manager)
    {
        float verticalVelocity = rigidbody.velocity.y + (manager.GravityScale * Physics2D.gravity.y * Time.deltaTime);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);

        if (IsFalling)
            manager.SwitchState(manager.FallingState);
    }


}