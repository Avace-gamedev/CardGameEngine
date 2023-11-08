namespace CardGame.Engine.Combats.Abstractions;

public class ShieldReceived
{
    public ShieldReceived(int shield)
    {
        Shield = shield;
    }

    public int Shield { get; }
}