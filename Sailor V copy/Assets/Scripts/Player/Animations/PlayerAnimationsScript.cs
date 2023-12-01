using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    [SerializeField] float returnToNormalAnimationTime = 1f;
    MovementStateManager stateManager;
    Animator animator;

    string currentAnimation = PlayerAnimations.IDLE;

    void Start()
    {
        var parent = transform.root;
        stateManager = parent.GetComponentInChildren<MovementStateManager>();
        animator = GetComponent<Animator>();

    }
    public void HandleDeadPosition()
    {
        stateManager.DeadState.HandleDeadPosition();
    }

    void ReturnAnimation()
    {
        animator.SetBool(PlayerActions.IS_SHOOTING, false);
    }
    public void InvokeReturnAnimation()
    {
        CancelInvoke(nameof(ReturnAnimation));
        Invoke(nameof(ReturnAnimation), returnToNormalAnimationTime);
    }

    public void SwitchAnimation(string newAnimation)
    {
        Debug.Log($"{currentAnimation} -> {newAnimation}");
        if (currentAnimation == newAnimation) return; // Guard
        currentAnimation = newAnimation;
        animator.Play(currentAnimation);
    }
}
