using System.Collections;
using UnityEngine;

public class PlayerDyingState : BaseState
{
    Rigidbody2D rigidbody;
    LayerMask groundLayer;
    Vector2 groundSurface;
    Transform playerTransform;

    public override void EnterState(PlayerStateController manager)
    {

        rigidbody = manager.myRb;
        playerTransform = manager.rootTransform;
        rigidbody.velocity = new Vector2(0, 0); // stop
        groundLayer = manager.groundMask;

        manager.animationHandler.SwitchState(PlayerAnimationName.DYING);
        groundSurface = GetGroundSurface();
    }

    public override void UpdateState(PlayerStateController manager) { }


    Vector2 GetGroundSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, Mathf.Infinity, groundLayer);
        return Physics2D.ClosestPoint(playerTransform.position, hit.collider);
    }

    public void SetPositionOnKO()
    {
        playerTransform.position = groundSurface;
    }

}