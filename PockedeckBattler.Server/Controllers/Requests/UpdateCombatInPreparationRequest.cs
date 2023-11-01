using System.ComponentModel.DataAnnotations;

namespace PockedeckBattler.Server.Controllers.Requests;

public class UpdateCombatInPreparationRequest
{
    [Required]
    public string PlayerName { get; set; }

    [Required]
    public string FrontCharacter { get; set; }

    [Required]
    public string BackCharacter { get; set; }
}
