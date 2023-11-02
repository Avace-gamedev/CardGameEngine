using System.ComponentModel.DataAnnotations;

namespace PockedeckBattler.Server.Rest.Combats.Requests;

public class UpdateCombatInPreparationRequest
{
    public bool? IsAi { get; set; }

    [Required]
    public string PlayerName { get; set; }

    [Required]
    public bool Ready { get; set; }

    public string? FrontCharacter { get; set; }
    public string? BackCharacter { get; set; }
}
