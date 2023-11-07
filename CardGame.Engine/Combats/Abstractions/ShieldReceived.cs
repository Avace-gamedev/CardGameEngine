namespace CardGame.Engine.Combats.Abstractions;

public class ShieldReceived
{
    public ShieldReceived(int health)
    {
        Health = health;
    }

    public int Health { get; }
}