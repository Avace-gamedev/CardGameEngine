using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Stores.Combats;

public class Combat
{
    public Combat(Guid id, string leftPlayerName, string rightPlayerName, CombatInstance instance)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
        RightPlayerName = rightPlayerName;
        Instance = instance;
    }

    public Guid Id { get; }
    public string LeftPlayerName { get; }
    public string RightPlayerName { get; }
    public CombatInstance Instance { get; }
}
