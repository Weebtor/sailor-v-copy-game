using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Components Required")]
    public Rigidbody2D myRb;
    public BoxCollider2D groundCollider;
    public LayerMask groundMask;

    [Header("Events")]
    public GameEvent OnPlayerStateChange;
    // public GameObject playerAnimation;
    [HideInInspector] public Transform rootTransform;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerAnimationController animationHandler;


    BaseState currentState;
    public PlayerIdleState IdleState = new();
    public PlayerCrouchingState CrouchingState = new();
    public PlayerJumpingState JumpingState = new();
    public PlayerDyingState DeadState = new();
    public PlayerFallingState FallingState = new();
    public PlayerWinState WinState = new();


    // variables
    [field: Header("Movement settings")]
    [field: SerializeField] public float GravityScale { get; private set; } = 1f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 5f;

    void Start()
    {
        rootTransform = transform.root.GetComponent<Transform>();
        animator = transform.root.GetComponentInChildren<Animator>();
        animationHandler = transform.root.GetComponentInChildren<PlayerAnimationController>();

        currentState = IdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
        OnPlayerStateChange.Raise(this, state);
    }


    public BaseState GetCurrentState()
    {
        return currentState;
    }

    [ContextMenu("KillPlayer")]
    public void KillPlayer()
    {
        SwitchState(this.DeadState);
    }

}
