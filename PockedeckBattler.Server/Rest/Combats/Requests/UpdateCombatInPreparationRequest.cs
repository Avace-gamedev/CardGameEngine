using System.ComponentModel.DataAnnotations;

namespace PockedeckBattler.Server.Rest.Combats.Requests;

public class UpdateCombatInPreparationRequest
{
    [Required]
    public string PlayerName { get; set; }

    public string? RandomSeed { get; set; }
}
