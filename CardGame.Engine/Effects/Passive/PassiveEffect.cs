namespace CardGame.Engine.Effects.Passive;

public abstract class PassiveEffect
{
    protected PassiveEffect(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }
}
