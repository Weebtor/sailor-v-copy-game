using UnityEngine;

public class PlayerGroundedState : MovementBaseState
{
    Rigidbody2D rigidbody;
    BoxCollider2D groundCollider;
    ContactFilter2D groundFilter;

    public override void EnterState(MovementStateManager manager)
    {
        rigidbody = manager.myRb;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        groundCollider = manager.groundCollider;
        Animator animator = manager.playerAnimation.GetComponent<Animator>();

        animator.SetTrigger(PlayerAnimations.Grounded);


        // rigidbody.transform.localScale = new Vector3(1, 1, 1); // grounded animation

        HandleGroundSnap(manager);
    }
    public override void UpdateState(MovementStateManager manager)
    {
        if (UserInputScript.instance.JumpJustPressed)
        {
            float verticalVelocity = Mathf.Sqrt(manager.JumpHeight * -2 * Physics2D.gravity.y * manager.GravityScale);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalVelocity);
            manager.SwitchState(manager.JumpingState);
        }
        else if (UserInputScript.instance.CrouchButtonDown || UserInputScript.instance.CrouchButtonHold)
        {
            manager.SwitchState(manager.CrouchingState);
        }

    }

    void HandleGroundSnap(MovementStateManager manager)
    {
        RaycastHit2D[] groundCastBuffer = new RaycastHit2D[1];
        int hit = groundCollider.Cast(Vector2.down, groundFilter, groundCastBuffer, 0f);
        if (hit > 0)
        {
            Vector2 surfacePosition = Physics2D.ClosestPoint(manager.transform.position, groundCastBuffer[0].collider);
            rigidbody.transform.position = surfacePosition;
        }
    }

}