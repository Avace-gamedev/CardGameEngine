namespace CardGame.Engine.Combats.Abstractions;

public class HealReceived
{
    public HealReceived(int health)
    {
        Health = health;
    }

    public int Health { get; }
}