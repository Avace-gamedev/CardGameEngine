namespace CardGame.Engine.Combats;

public class ShieldReceived
{
    public ShieldReceived(int health)
    {
        Health = health;
    }

    public int Health { get; }
}