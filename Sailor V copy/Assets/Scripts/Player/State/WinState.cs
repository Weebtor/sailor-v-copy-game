public class PlayerWinState : BaseState
{
    public override void EnterState(PlayerStateManager manager)
    {
        manager.animationHandler.SwitchState(PlayerAnimationName.WIN);
    }

    public override void UpdateState(PlayerStateManager manager)
    {
    }
}
