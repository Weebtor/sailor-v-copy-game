using UnityEngine;

public class PlayerGroundedState : MovementBaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    LayerMask groundMask;
    ContactFilter2D groundFilter;

    public override void EnterState(MovementStateManager movement)
    {
        rigidbody = movement.myRb;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        groundCollider = movement.groundCollider;
        HandleGroundSnap(movement);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float verticalVelocity = Mathf.Sqrt(movement.GetJumpHeight() * -2 * Physics2D.gravity.y * movement.GetGravityScale());
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);
            movement.SwitchState(movement.JumpingState);
        }


    }

    void HandleGroundSnap(MovementStateManager movement)
    {
        RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];
        int hit = groundCollider.Cast(Vector2.down, groundFilter, groundCastBuffer, 0f);
        if (hit > 0)
        {
            Vector2 surfacePosition = Physics2D.ClosestPoint(movement.transform.position + Vector3.up, groundCastBuffer[0].collider);
            movement.transform.position = surfacePosition;
        }


    }

}