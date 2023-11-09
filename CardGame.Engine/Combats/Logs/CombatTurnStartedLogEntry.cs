namespace CardGame.Engine.Combats.Logs;

public class CombatTurnStartedLogEntry : CombatLogEntry
{
    public CombatTurnStartedLogEntry(int turn)
    {
        Turn = turn;
    }

    public int Turn { get; }
}
