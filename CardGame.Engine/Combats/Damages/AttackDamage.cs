namespace CardGame.Engine.Combats.Damages;

public class AttackDamage
{
    public AttackDamage(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }
}
