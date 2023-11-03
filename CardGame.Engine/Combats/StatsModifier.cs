namespace CardGame.Engine.Combats;

public class StatsModifier
{
    public static StatsModifier None => new() { HealthAdditiveModifier = 0, ApCostAdditiveModifier = 0, DamageAdditiveModifier = 0, ResistanceAdditiveModifier = 0 };

    public int HealthAdditiveModifier { get; init; }
    public int ApCostAdditiveModifier { get; init; }
    public int DamageAdditiveModifier { get; init; }
    public int ResistanceAdditiveModifier { get; init; }

    public static StatsModifier Combine(StatsModifier modifier1, StatsModifier modifier2)
    {
        return new StatsModifier
        {
            HealthAdditiveModifier = modifier1.HealthAdditiveModifier + modifier2.HealthAdditiveModifier,
            ApCostAdditiveModifier = modifier1.ApCostAdditiveModifier + modifier2.ApCostAdditiveModifier,
            DamageAdditiveModifier = modifier1.DamageAdditiveModifier + modifier2.DamageAdditiveModifier,
            ResistanceAdditiveModifier = modifier1.ResistanceAdditiveModifier + modifier2.ResistanceAdditiveModifier
        };
    }
}
