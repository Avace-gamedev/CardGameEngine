using PockedeckBattler.Server.Stores;

namespace PockedeckBattler.Server.Controllers.Views;

public class CombatInPreparationView
{
    public CombatInPreparationView(Guid id, string leftPlayerName)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
    }

    public Guid Id { get; }
    public string LeftPlayerName { get; }
    public string? LeftFrontCharacter { get; set; }
    public string? LeftBackCharacter { get; set; }

    public string? RightPlayerName { get; set; }
    public string? RightFrontCharacter { get; set; }
    public string? RightBackCharacter { get; set; }
}

public static class CombatInPreparationViewMappingExtensions
{
    public static CombatInPreparationView View(this StoredCombatInPreparation combat)
    {
        return new CombatInPreparationView(combat.Id, combat.LeftPlayerName)
        {
            LeftFrontCharacter = combat.LeftFrontCharacter,
            LeftBackCharacter = combat.LeftBackCharacter,
            RightPlayerName = combat.RightPlayerName,
            RightFrontCharacter = combat.RightFrontCharacter,
            RightBackCharacter = combat.RightBackCharacter
        };
    }
}
