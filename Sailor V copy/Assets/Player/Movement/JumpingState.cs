using UnityEngine;

public class PlayerJumpingState : MovementBaseState
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
        Animator animator = manager.playerAnimation.GetComponent<Animator>();
        animator.SetTrigger("ToJumping");
        // rigidbody.transform.localScale = new Vector3(1, 1.1f, 1); // jumping animation


    }
    public override void UpdateState(MovementStateManager manager)
    {
        float verticalVelocity = rigidbody.velocity.y + (manager.GetGravityScale() * Physics2D.gravity.y * Time.deltaTime); // gravity
        if (!IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);
        }
        else
        {
            manager.SwitchState(manager.GroundedState);
        }
    }


    bool IsGrounded()
    {
        if (rigidbody.velocity.y > 0) return false;

        int hit = groundCollider.Cast(Vector2.down, groundFilter, groundCastBuffer, 0f);
        if (hit > 0)
        {
            return true;
        }
        return false;
    }


}