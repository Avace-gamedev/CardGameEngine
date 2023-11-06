using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Views;

public class CombatInPreparationView
{
    public CombatInPreparationView(Guid id, string leftPlayerName)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
    }

    public Guid Id { get; }
    public string? RandomSeed { get; set; }

    public string LeftPlayerName { get; }
    public string? LeftFrontCharacter { get; set; }
    public string? LeftBackCharacter { get; set; }
    public bool LeftReady { get; set; }

    public bool RightPlayerIsAi { get; set; }
    public string? RightPlayerName { get; set; }
    public string? RightFrontCharacter { get; set; }
    public string? RightBackCharacter { get; set; }
    public bool RightReady { get; set; }
}

public static class CombatInPreparationViewMappingExtensions
{
    public static CombatInPreparationView View(this CombatInPreparation combat)
    {
        return new CombatInPreparationView(combat.Id, combat.LeftPlayerName)
        {
            RandomSeed = combat.RandomSeed,
            LeftFrontCharacter = combat.LeftFrontCharacter,
            LeftBackCharacter = combat.LeftBackCharacter,
            LeftReady = combat.LeftReady,
            RightPlayerIsAi = combat.RightPlayerIsAi,
            RightPlayerName = combat.RightPlayerName,
            RightFrontCharacter = combat.RightFrontCharacter,
            RightBackCharacter = combat.RightBackCharacter,
            RightReady = combat.RightReady
        };
    }
}
