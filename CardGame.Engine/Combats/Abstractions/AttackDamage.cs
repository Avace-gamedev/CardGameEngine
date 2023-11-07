namespace CardGame.Engine.Combats.Abstractions;

public class AttackDamage
{
    public AttackDamage(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }
}
