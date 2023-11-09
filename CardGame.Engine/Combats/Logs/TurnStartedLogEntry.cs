namespace CardGame.Engine.Combats.Logs;

public class TurnStartedLogEntry : CombatLogEntry
{
    public TurnStartedLogEntry(int turn)
    {
        Turn = turn;
    }

    public int Turn { get; }
}
