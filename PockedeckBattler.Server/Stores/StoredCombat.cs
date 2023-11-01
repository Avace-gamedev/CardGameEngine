using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Stores;

public class StoredCombat
{
    public StoredCombat(Guid id, string leftPlayerName, string rightPlayerName, CombatInstance combat)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
        RightPlayerName = rightPlayerName;
        Combat = combat;
    }

    public Guid Id { get; }
    public string LeftPlayerName { get; }
    public string RightPlayerName { get; }
    public CombatInstance Combat { get; }
}
