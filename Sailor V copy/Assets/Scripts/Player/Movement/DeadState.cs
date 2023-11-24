using UnityEngine;

public class PlayerDeadState : MovementBaseState
{

    public override void EnterState(MovementStateManager manager)
    {
        Animator animator = manager.playerAnimation.GetComponent<Animator>();
        animator.SetTrigger(PlayerAnimations.Dead);

    }

    public override void UpdateState(MovementStateManager manager)
    {
    }

}