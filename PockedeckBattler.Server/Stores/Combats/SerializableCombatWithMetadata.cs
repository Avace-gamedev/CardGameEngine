using CardGame.Engine.Combats;
using CardGame.Engine.Combats.History;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Stores.Combats;

public class SerializableCombatWithMetadata
{
    public Guid? Id { get; init; }
    public CombatInPreparation? Configuration { get; init; }
    public string? LeftPlayerName { get; init; }
    public string? RightPlayerName { get; init; }
    public CombatHistory? CombatHistory { get; init; }

    public CombatInstanceWithMetadata? Restore()
    {
        if (!Id.HasValue || string.IsNullOrWhiteSpace(LeftPlayerName) || string.IsNullOrWhiteSpace(RightPlayerName) || CombatHistory == null)
        {
            return null;
        }

        CombatInstance instance = CombatHistory.Replay();

        return new CombatInstanceWithMetadata(Id.Value, LeftPlayerName, RightPlayerName, instance, Configuration);
    }

    public static SerializableCombatWithMetadata From(CombatInstanceWithMetadata combat)
    {
        return new SerializableCombatWithMetadata
        {
            Id = combat.Id,
            Configuration = combat.Configuration,
            LeftPlayerName = combat.LeftPlayerName,
            RightPlayerName = combat.RightPlayerName,
            CombatHistory = combat.Instance.History
        };
    }
}
