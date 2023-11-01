using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.Content.Characters;
using PockedeckBattler.Server.Controllers.Requests;
using PockedeckBattler.Server.Controllers.Views;
using PockedeckBattler.Server.Stores;

namespace PockedeckBattler.Server.Controllers;

[ApiController]
[Route("combats")]
public class CombatsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public ActionResult<CombatView> Get(Guid id, [Required] string playerName)
    {
        StoredCombat combat = CombatStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet]
    public IEnumerable<PlayerCombatView> GetCombatsOfPlayer([Required] string playerName)
    {
        return CombatStore.GetCombatsInvolvingPlayer(playerName).Select(c => c.PlayerView(c.LeftPlayerName == playerName ? CombatSide.Left : CombatSide.Right));
    }

    [HttpGet("in-preparation/{id:guid}")]
    public ActionResult<CombatInPreparationView> GetCombatInPreparation(Guid id, [Required] string playerName)
    {
        StoredCombatInPreparation combat = CombatStore.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet("in-preparation")]
    public IEnumerable<CombatInPreparationView> GetCombatsInPreparationOfPlayer([Required] string playerName)
    {
        return CombatStore.GetCombatsInPreparationInvolvingPlayer(playerName).Select(c => c.View());
    }

    [HttpPost]
    public CombatInPreparationView CreateCombat([Required] string playerName)
    {
        StoredCombatInPreparation combatInPreparation = new(Guid.NewGuid(), playerName);
        CombatStore.SaveCombatInPreparation(combatInPreparation);

        return combatInPreparation.View();
    }

    [HttpPost("in-preparation/{id:guid}")]
    public void UpdateCombatInPreparation(Guid id, UpdateCombatInPreparationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PlayerName))
        {
            throw new Exception("Player name cannot be empty");
        }

        StoredCombatInPreparation combatInPreparation = CombatStore.RequireCombatInPreparation(id);

        if (request.PlayerName == combatInPreparation.LeftPlayerName)
        {
            combatInPreparation.LeftFrontCharacter = request.FrontCharacter;
            combatInPreparation.LeftBackCharacter = request.BackCharacter;
            combatInPreparation.LeftReady = request.Ready;
        }
        else if (request.PlayerName == combatInPreparation.RightPlayerName || combatInPreparation.RightPlayerName == null)
        {
            combatInPreparation.RightPlayerName = request.PlayerName;
            combatInPreparation.RightFrontCharacter = request.FrontCharacter;
            combatInPreparation.RightBackCharacter = request.BackCharacter;
            combatInPreparation.RightReady = request.Ready;
        }
        else
        {
            throw new Exception($"Player {request.PlayerName} is not part of combat {id}");
        }

        CombatStore.SaveCombatInPreparation(combatInPreparation);
    }

    [HttpPost("in-preparation/{id:guid}/leave")]
    public ActionResult LeaveCombatInPreparation(Guid id, [Required] string playerName)
    {
        StoredCombatInPreparation combat = CombatStore.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        if (playerName == combat.LeftPlayerName)
        {
            CombatStore.DeleteCombatInPreparation(id);
        }
        else if (playerName == combat.RightPlayerName)
        {
            combat.RightPlayerName = null;
            combat.RightFrontCharacter = null;
            combat.RightBackCharacter = null;
        }
        else
        {
            throw new Exception($"Player {playerName} is not part of combat {id}");
        }

        CombatStore.SaveCombatInPreparation(combat);
        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    public ActionResult<CombatView> StartCombat(Guid combatId, [Required] string playerName)
    {
        StoredCombatInPreparation inPreparation = CombatStore.RequireCombatInPreparation(combatId);
        if (!PlayerInCombat(inPreparation, playerName))
        {
            return NotFound();
        }

        if (inPreparation.LeftFrontCharacter == null && inPreparation.LeftBackCharacter == null)
        {
            return BadRequest("Left side doesn't have any character");
        }

        if (inPreparation.RightPlayerName == null)
        {
            return BadRequest("Right side not configured");
        }

        if (inPreparation.RightFrontCharacter == null && inPreparation.RightBackCharacter == null)
        {
            return BadRequest("Right side doesn't have any character");
        }

        if (!inPreparation.LeftReady)
        {
            return BadRequest("Left side not ready");
        }

        if (!inPreparation.RightReady)
        {
            return BadRequest("Right side not ready");
        }

        Character? leftFrontCharacter = inPreparation.LeftFrontCharacter == null ? null : Characters.RequireByName(inPreparation.LeftFrontCharacter);
        Character? leftBackCharacter = inPreparation.LeftBackCharacter == null ? null : Characters.RequireByName(inPreparation.LeftBackCharacter);

        Character? rightFrontCharacter = inPreparation.RightFrontCharacter == null ? null : Characters.RequireByName(inPreparation.RightFrontCharacter);
        Character? rightBackCharacter = inPreparation.RightBackCharacter == null ? null : Characters.RequireByName(inPreparation.RightBackCharacter);

        CombatInstance combatInstance = new(
            new[] { leftFrontCharacter, leftBackCharacter }.Where(c => c != null).Select(c => c!).ToArray(),
            new[] { rightFrontCharacter, rightBackCharacter }.Where(c => c != null).Select(c => c!).ToArray(),
            new CombatOptions()
        );

        StoredCombat combat = new(Guid.NewGuid(), inPreparation.LeftPlayerName, inPreparation.RightPlayerName, combatInstance);
        CombatStore.SaveCombat(combat);

        return combat.View();
    }

    [HttpPost("{id:guid}/{side}/play/{index:int}")]
    public ActionResult PlayCard(Guid id, [Required] string playerName, int index)
    {
        StoredCombat combat = CombatStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Combat.PlayCardAt(side, index);

        CombatStore.SaveCombat(combat);
        return NoContent();
    }

    [HttpPost("{id:guid}/{side}/end-turn")]
    public ActionResult EndTurn(Guid id, [Required] string playerName)
    {
        StoredCombat combat = CombatStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Combat.EndSideTurnAndStartNextOne(side);

        CombatStore.SaveCombat(combat);
        return NoContent();
    }

    static bool PlayerInCombat(StoredCombat combat, [Required] string playerName)
    {
        return PlayerInCombat(combat, playerName, out _);
    }

    static bool PlayerInCombat(StoredCombat combat, [Required] string playerName, out CombatSide side)
    {
        return PlayerInCombat(playerName, combat.LeftPlayerName, combat.RightPlayerName, out side);
    }

    static bool PlayerInCombat(StoredCombatInPreparation combat, [Required] string playerName)
    {
        return PlayerInCombat(combat, playerName, out _);
    }

    static bool PlayerInCombat(StoredCombatInPreparation combat, [Required] string playerName, out CombatSide side)
    {
        return PlayerInCombat(playerName, combat.LeftPlayerName, combat.RightPlayerName, out side);
    }

    static bool PlayerInCombat([Required] string playerName, string leftPlayer, string? rightPlayer, out CombatSide side)
    {
        if (leftPlayer == playerName)
        {
            side = CombatSide.Left;
            return true;
        }

        if (rightPlayer == playerName)
        {
            side = CombatSide.Right;
            return true;
        }

        side = CombatSide.None;
        return false;
    }
}
