using System.Collections;
using UnityEngine;

public class PlayerFallingState : BaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    ContactFilter2D groundFilter;


    private RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];

    public override void EnterState(PlayerStateController manager)
    {
        rigidbody = manager.myRb;
        groundCollider = manager.groundCollider;
        groundFilter.SetLayerMask(manager.groundMask);
        manager.animationController.SwitchState(PlayerAnimationName.FALLING);
    }

    public override void UpdateState(PlayerStateController manager)
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
