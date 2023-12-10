using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] float returnToNormalAnimationTime = 1f;

    MovementStateManager stateManager;
    Animator animator;
    PlayerAnimationLayer currentLayer = PlayerAnimationLayer.Normal;
    string currentAnimation = PlayerAnimation.IDLE;

    void Start()
    {
        var parent = transform.root;
        stateManager = parent.GetComponentInChildren<MovementStateManager>();
        animator = GetComponent<Animator>();
        Debug.Log(animator.GetLayerName(0));

    }
    public void SwitchLayer(PlayerAnimationLayer newLayer)
    {
        if (newLayer == currentLayer) return;

        animator.SetLayerWeight((int)currentLayer, 0);
        animator.SetLayerWeight((int)newLayer, 1);
        currentLayer = newLayer;

    }
    public void SwitchState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return; // Guard

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void HandleDeadPosition()
    {
        stateManager.DeadState.HandleDeadPosition();
    }
    public void HandleShootAnimation()
    {
        SwitchLayer(PlayerAnimationLayer.GunWeapon);
        InvokeNormalAnimation();
    }

    public void InvokeNormalAnimation()
    {
        CancelInvoke(nameof(StartNormalAnimation));
        Invoke(nameof(StartNormalAnimation), returnToNormalAnimationTime);
    }
    public void StartNormalAnimation()
    {
        SwitchLayer(PlayerAnimationLayer.Normal);
    }

}
