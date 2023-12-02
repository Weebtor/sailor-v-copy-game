using System.Collections;
using UnityEngine;

public class PlayerDyingState : MovementBaseState
{
    Rigidbody2D rigidbody;
    LayerMask groundLayer;
    Vector2 groundSurface;
    Transform playerTransform;

    public override void EnterState(MovementStateManager manager)
    {
        rigidbody = manager.myRb;
        rigidbody.velocity = new Vector2(0, 0); // stop
        groundLayer = manager.groundMask;

        manager.animationHandler.SwitchState(PlayerAnimation.DYING);
        groundSurface = GetGroundSurface();


    }

    public override void UpdateState(MovementStateManager manager) { }


    Vector2 GetGroundSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, Mathf.Infinity, groundLayer);
        return Physics2D.ClosestPoint(playerTransform.position, hit.collider);
    }

    public void HandleDeadPosition() { playerTransform.position = groundSurface; }

}