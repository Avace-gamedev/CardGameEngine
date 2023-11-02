using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Stores.Combats;

public class CombatWithMetadata
{
    public CombatWithMetadata(Guid id, string leftPlayerName, string rightPlayerName, CombatInstance instance, CombatInPreparation? configuration = null)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
        RightPlayerName = rightPlayerName;
        Instance = instance;
        Configuration = configuration;
    }

    public Guid Id { get; }
    public CombatInPreparation? Configuration { get; }
    public string LeftPlayerName { get; }
    public string RightPlayerName { get; }
    public CombatInstance Instance { get; }
}
