public class PlayerWinState : BaseState
{
    public override void EnterState(PlayerStateController manager)
    {
        manager.animationHandler.SwitchState(PlayerAnimationName.WIN);
    }

    public override void UpdateState(PlayerStateController manager)
    {
    }
}
