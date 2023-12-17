public static class PlayerAnimationName
{
    public const string IDLE = "Player_Idle";
    public const string CROUCHING = "Player_Crouching";
    public const string JUMPING = "Player_Jumping";
    public const string FALLING = "Player_Falling";
    public const string DYING = "Player_Dying";
    public const string WIN = "Player_Win";

}

public static class PlayerActions
{
    public const string SHOOT = "Shoot";

}

public enum PlayerAnimationLayer
{
    Normal,
    Aiming
}