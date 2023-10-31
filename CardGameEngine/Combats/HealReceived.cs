namespace CardGameEngine.Combats;

public class HealReceived
{
    public HealReceived(int health)
    {
        Health = health;
    }

    public int Health { get; }
}