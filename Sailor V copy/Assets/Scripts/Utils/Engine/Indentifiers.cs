namespace General
{

    public static class Tags
    {
        // public const string InGame_EffectAreas = "InGame_EffectAreas";
        // public const string InGame_Projectil = "InGame_Projectil";
    }

    public enum Layers
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        Ground = 6,
        Hurtbox = 7,
        Hitbox = 8,
    }
}