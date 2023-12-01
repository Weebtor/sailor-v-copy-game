using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public Rigidbody2D myRb;
    public BoxCollider2D groundCollider;
    public LayerMask groundMask;
    // public GameObject playerAnimation;
    [HideInInspector] public Transform rootTransform;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerAnimationScript animationHandler;

    MovementBaseState currentState;

    public PlayerIdleState IdleState = new();
    public PlayerCrouchingState CrouchingState = new();
    public PlayerJumpingState JumpingState = new();
    public PlayerDyingState DeadState = new();
    public PlayerFallingState FallingState = new();


    // variables
    [field: SerializeField] public float GravityScale { get; private set; } = 1f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 5f;

    void Start()
    {
        rootTransform = transform.root.GetComponent<Transform>();
        animator = transform.root.GetComponentInChildren<Animator>();
        animationHandler = transform.root.GetComponentInChildren<PlayerAnimationScript>();

        currentState = IdleState;
        currentState.EnterState(this);
    }

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

    [ContextMenu("KillPlayer")]
    public void KillPlayer()
    {
        SwitchState(this.DeadState);
    }

}
