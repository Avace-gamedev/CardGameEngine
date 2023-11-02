using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.GameContent.Characters;
using PockedeckBattler.Server.Rest.Requests;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.Rest;

[ApiController]
[Route("combats")]
public class CombatsController : ControllerBase
{
    readonly ICombatInPreparationService _combatInPreparationService;
    readonly ICombatService _combatService;

    public CombatsController(ICombatService combatService, ICombatInPreparationService combatInPreparationService)
    {
        _combatService = combatService;
        _combatInPreparationService = combatInPreparationService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CombatView>> Get(Guid id, [Required] string playerName)
    {
        Combat combat = await _combatService.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet]
    public async IAsyncEnumerable<PlayerCombatView> GetCombatsOfPlayer([Required] string playerName)
    {
        await foreach (Combat combat in _combatService.GetCombatsInvolvingPlayer(playerName))
        {
            if (combat.LeftPlayerName == playerName)
            {
                yield return combat.PlayerView(CombatSide.Left);
            }
            else
            {
                yield return combat.PlayerView(CombatSide.Right);
            }
        }
    }

    [HttpGet("in-preparation/{id:guid}")]
    public async Task<ActionResult<CombatInPreparationView>> GetCombatInPreparation(Guid id, [Required] string playerName)
    {
        CombatInPreparation combat = await _combatInPreparationService.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet("in-preparation")]
    public async IAsyncEnumerable<CombatInPreparationView> GetCombatsInPreparationOfPlayer([Required] string playerName)
    {
        await foreach (CombatInPreparation combatInPreparation in _combatInPreparationService.GetCombatsInPreparationInvolvingPlayer(playerName))
        {
            yield return combatInPreparation.View();
        }
    }

    [HttpPost]
    public async Task<CombatInPreparationView> CreateCombat([Required] string playerName)
    {
        CombatInPreparation combatInPreparation = new(Guid.NewGuid(), playerName);
        await _combatInPreparationService.SaveCombatInPreparation(combatInPreparation);

        return combatInPreparation.View();
    }

    [HttpPost("in-preparation/{id:guid}")]
    public async Task UpdateCombatInPreparation(Guid id, UpdateCombatInPreparationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PlayerName))
        {
            throw new Exception("Player name cannot be empty");
        }

        CombatInPreparation combatInPreparation = await _combatInPreparationService.RequireCombatInPreparation(id);

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

        await _combatInPreparationService.SaveCombatInPreparation(combatInPreparation);
    }

    [HttpPost("in-preparation/{id:guid}/leave")]
    public async Task<ActionResult> LeaveCombatInPreparation(Guid id, [Required] string playerName)
    {
        CombatInPreparation combat = await _combatInPreparationService.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        if (playerName == combat.LeftPlayerName)
        {
            await _combatInPreparationService.DeleteCombatInPreparation(id);
        }
        else if (playerName == combat.RightPlayerName)
        {
            combat.RightPlayerName = null;
            combat.RightFrontCharacter = null;
            combat.RightBackCharacter = null;
            await _combatInPreparationService.SaveCombatInPreparation(combat);
        }
        else
        {
            throw new Exception($"Player {playerName} is not part of combat {id}");
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    public async Task<ActionResult<CombatView>> StartCombat(Guid combatId, [Required] string playerName)
    {
        CombatInPreparation inPreparation = await _combatInPreparationService.RequireCombatInPreparation(combatId);
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

        Combat combat = new(Guid.NewGuid(), inPreparation.LeftPlayerName, inPreparation.RightPlayerName, combatInstance);
        await _combatService.SaveCombat(combat);

        return combat.View();
    }

    [HttpPost("{id:guid}/play/{index:int}")]
    public async Task<ActionResult> PlayCard(Guid id, [Required] string playerName, int index)
    {
        Combat combat = await _combatService.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Instance.PlayCardAt(side, index);

        await _combatService.SaveCombat(combat);
        return NoContent();
    }

    [HttpPost("{id:guid}/end-turn")]
    public async Task<ActionResult> EndTurn(Guid id, [Required] string playerName)
    {
        Combat combat = await _combatService.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Instance.EndSideTurnAndStartNextOne(side);

        await _combatService.SaveCombat(combat);
        return NoContent();
    }

    static bool PlayerInCombat(Combat combat, [Required] string playerName)
    {
        return PlayerInCombat(combat, playerName, out _);
    }

    static bool PlayerInCombat(Combat combat, [Required] string playerName, out CombatSide side)
    {
        return PlayerInCombat(playerName, combat.LeftPlayerName, combat.RightPlayerName, out side);
    }

    static bool PlayerInCombat(CombatInPreparation combat, [Required] string playerName)
    {
        return PlayerInCombat(combat, playerName, out _);
    }

    static bool PlayerInCombat(CombatInPreparation combat, [Required] string playerName, out CombatSide side)
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
