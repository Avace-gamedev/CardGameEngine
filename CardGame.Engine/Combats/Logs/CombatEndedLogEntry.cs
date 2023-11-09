using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class CombatEndedLogEntry : CombatLogEntry
{
    public CombatEndedLogEntry(CombatSide winner)
    {
        Winner = winner;
    }

    public CombatSide Winner { get; }
}
