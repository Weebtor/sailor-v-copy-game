using System.Collections;
using UnityEngine;

public class PlayerFallingState : MovementBaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    ContactFilter2D groundFilter;


    private RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];

    public override void EnterState(MovementStateManager manager)
    {
        rigidbody = manager.myRb;
        groundCollider = manager.groundCollider;
        groundFilter.SetLayerMask(manager.groundMask);
        manager.animationHandler.SwitchState(PlayerAnimation.FALLING);
    }

    public override void UpdateState(MovementStateManager manager)
    {
        float verticalVelocity = rigidbody.velocity.y + (manager.GravityScale * Physics2D.gravity.y * Time.deltaTime);

        if (!IsGrounded())
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);
        else
            manager.SwitchState(manager.IdleState);

    }
    bool IsGrounded()
    {
        int hit = groundCollider.Cast(Vector2.down, groundFilter, groundCastBuffer, 0f);
        return hit > 0;
    }
}
