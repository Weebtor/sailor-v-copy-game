using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] float returnToNormalAnimationTime = 1f;

    [SerializeField] Color flashColor = Color.white;
    [SerializeField] float flashTime = 0.125f;

    Material material;
    PlayerStateManager stateManager;
    Animator animator;
    PlayerAnimationLayer currentLayer = PlayerAnimationLayer.Normal;
    string currentAnimation = PlayerAnimationName.IDLE;

    void Start()
    {
        var parent = transform.root;
        stateManager = parent.GetComponentInChildren<PlayerStateManager>();
        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
        material.SetColor("_FlashColor", flashColor);
    }
    public void SwitchLayer(PlayerAnimationLayer newLayer)
    {
        if (newLayer == currentLayer) return;

        animator.SetLayerWeight((int)currentLayer, 0);
        animator.SetLayerWeight((int)newLayer, 1);
        currentLayer = newLayer;

    }
    // 
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

    // return to normal animation from aiming
    public void InvokeNormalAnimation()
    {
        CancelInvoke(nameof(StartNormalAnimation));
        Invoke(nameof(StartNormalAnimation), returnToNormalAnimationTime);
    }
    public void StartNormalAnimation()
    {
        SwitchLayer(PlayerAnimationLayer.Normal);
    }

    // flash damage
    public void TakeDamageAnimation()
    {
        StartCoroutine(DagemeFlash());
    }
    IEnumerator DagemeFlash()
    {
        material.SetFloat("_FlashValue", 1);
        yield return new WaitForSeconds(flashTime);
        material.SetFloat("_FlashValue", 0);
        yield return new WaitForSeconds(flashTime);
        material.SetFloat("_FlashValue", 1);
        yield return new WaitForSeconds(flashTime);
        material.SetFloat("_FlashValue", 0);
        yield return new WaitForSeconds(flashTime);

    }
}
