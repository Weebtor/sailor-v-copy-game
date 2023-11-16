using UnityEngine;

public class PlayerJumpingState : MovementBaseState
{

    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    LayerMask groundMask;
    ContactFilter2D groundFilter;

    private RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];

    public override void EnterState(MovementStateManager movement)
    {
        rigidbody = movement.myRb;
        groundCollider = movement.groundCollider;
        groundMask = movement.groundMask;
        groundFilter.SetLayerMask(groundMask);

        Debug.Log("hello from jumping State");
    }
    public override void UpdateState(MovementStateManager movement)
    {
        float verticalVelocity = rigidbody.velocity.y + (movement.GetGravityScale() * Physics2D.gravity.y * Time.deltaTime); // gravity
        if (!IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);
        }
        else
        {
            movement.SwitchState(movement.GroundedState);
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