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
    public PlayerDeadState DeadState = new();


    // variables
    [field: SerializeField] public float GravityScale { get; private set; } = 1f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 5f;

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

    public MovementBaseState GetCurrentState()
    {
        return currentState;
    }
}
