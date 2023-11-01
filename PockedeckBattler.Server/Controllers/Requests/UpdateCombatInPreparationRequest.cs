using System.ComponentModel.DataAnnotations;

namespace PockedeckBattler.Server.Controllers.Requests;

public class UpdateCombatInPreparationRequest
{
    [Required]
    public string PlayerName { get; set; }

    [Required]
    public bool Ready { get; set; }

    public string? FrontCharacter { get; set; }
    public string? BackCharacter { get; set; }
}
