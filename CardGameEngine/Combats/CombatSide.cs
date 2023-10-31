namespace CardGameEngine.Combats;

public enum CombatSide
{
    None,
    Left,
    Right
}

public static class CombatSideExtensions
{
    public static CombatSide OtherSide(this CombatSide side)
    {
        return side switch
        {
            CombatSide.None => CombatSide.None,
            CombatSide.Left => CombatSide.Right,
            CombatSide.Right => CombatSide.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };
    }
}
