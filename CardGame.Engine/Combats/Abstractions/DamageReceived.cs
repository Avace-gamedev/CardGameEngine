namespace CardGame.Engine.Combats.Abstractions;

public class DamageReceived
{
    public DamageReceived(int health, int shield)
    {
        Health = health;
        Shield = shield;
    }

    public int Health { get; }
    public int Shield { get; }
}
