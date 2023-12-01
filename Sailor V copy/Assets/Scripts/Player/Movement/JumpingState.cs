using UnityEngine;

public class PlayerJumpingState : MovementBaseState
{
    Rigidbody2D rigidbody;
    Animator animator;
    bool IsFalling => rigidbody.velocity.y <= 0;

    public override void EnterState(MovementStateManager manager)
    {
        rigidbody = manager.myRb;
        float initialVerticalVelocity = Mathf.Sqrt(manager.JumpHeight * -2 * Physics2D.gravity.y * manager.GravityScale);
        rigidbody.velocity = new Vector2(rigidbody.velocity.y, initialVerticalVelocity);

        manager.animationHandler.SwitchAnimation(PlayerAnimations.JUMPING);
    }
    public override void UpdateState(MovementStateManager manager)
    {
        float verticalVelocity = rigidbody.velocity.y + (manager.GravityScale * Physics2D.gravity.y * Time.deltaTime);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);

        if (IsFalling)
            manager.SwitchState(manager.FallingState);
    }


}