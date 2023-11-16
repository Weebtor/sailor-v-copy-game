using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public Rigidbody2D myRb;
    public BoxCollider2D groundCollider;
    public LayerMask groundMask;



    MovementBaseState currentState;
    public PlayerJumpingState JumpingState = new();
    public PlayerGroundedState GroundedState = new();


    // variables
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 5f;
    public float GetGravityScale() { return gravityScale; }
    public float GetJumpHeight() { return jumpHeight; }

    void Start()
    {
        currentState = JumpingState;
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
