using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public Rigidbody2D myRb;
    public BoxCollider2D groundCollider;
    public LayerMask groundMask;
    public GameObject playerAnimation;


    MovementBaseState currentState;
    public PlayerGroundedState GroundedState = new();
    public PlayerCrouchingState CrouchingState = new();
    public PlayerJumpingState JumpingState = new();


    // variables
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 5f;
    public float GetGravityScale() { return gravityScale; }
    public float GetJumpHeight() { return jumpHeight; }

    void Start()
    {
        currentState = GroundedState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
