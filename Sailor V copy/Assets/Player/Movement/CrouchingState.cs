using UnityEngine;

public class PlayerCrouchingState : MovementBaseState
{
    Rigidbody2D rigidbody;

    public override void EnterState(MovementStateManager manager)
    {
        rigidbody = manager.myRb;
        Animator animator = manager.playerAnimation.GetComponent<Animator>();
        animator.SetTrigger("ToCrouch");
        Debug.Log("Crouching");
        // rigidbody.transform.localScale = new Vector3(1, 0.5f, 1); // crouch animation
    }

    public override void UpdateState(MovementStateManager manager)
    {

        if (Input.GetKey(KeyCode.S))
        {
            Animator animator = manager.playerAnimation.GetComponent<Animator>();
        }
        else
        {
            manager.SwitchState(manager.GroundedState);
        }


    }

}