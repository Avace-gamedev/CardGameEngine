using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.Rest.Combats.Requests;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.Rest.Combats;

[ApiController]
[Route("combats")]
public class CombatsController : ControllerBase
{
    readonly ICombatInPreparationService _combatInPreparationService;
    readonly ICombatService _combatsService;
    readonly ILogger<CombatsController> _logger;

    public CombatsController(ICombatService combatsService, ICombatInPreparationService combatInPreparationService, ILogger<CombatsController> logger)
    {
        _combatsService = combatsService;
        _combatInPreparationService = combatInPreparationService;
        _logger = logger;
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
            if (request.IsAi == true)
            {
                _logger.LogWarning("Left player cannot be AI, value of IsAi will be ignored");
            }

            combatInPreparation.LeftFrontCharacter = request.FrontCharacter;
            combatInPreparation.LeftBackCharacter = request.BackCharacter;
            combatInPreparation.LeftReady = request.Ready;
        }
        else if (request.PlayerName == combatInPreparation.RightPlayerName || combatInPreparation.RightPlayerName == null)
        {
            combatInPreparation.RightPlayerIsAi = request.IsAi ?? false;
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
            await _combatInPreparationService.DeleteCombatInPreparation(combat);
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
    public async Task<ActionResult<Guid>> StartCombat(Guid id, [Required] string playerName)
    {
        CombatInPreparation inPreparation = await _combatInPreparationService.RequireCombatInPreparation(id);
        if (!PlayerInCombat(inPreparation, playerName))
        {
            return NotFound();
        }

        CombatWithMetadata combat = await _combatsService.CreateCombat(inPreparation);

        await _combatInPreparationService.DeleteCombatInPreparation(inPreparation);

        return combat.Id;
    }

    [HttpGet]
    public async IAsyncEnumerable<PlayerCombatView> GetCombatsOfPlayer([Required] string playerName)
    {
        await foreach (CombatWithMetadata combat in _combatsService.GetCombatsInvolvingPlayer(playerName))
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlayerCombatView>> GetCombat(Guid id, [Required] string playerName)
    {
        CombatWithMetadata? combat = await _combatsService.GetCombat(id);
        if (combat == null)
        {
            return NotFound();
        }

        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        return combat.PlayerView(side);
    }

    [HttpPost("{id:guid}/play/{index:int}")]
    public async Task<ActionResult> PlayCard(Guid id, [Required] string playerName, int index)
    {
        CombatWithMetadata combat = await _combatsService.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Instance.PlayCard(side, index);

        await _combatsService.SaveCombat(combat);
        return NoContent();
    }

    [HttpPost("{id:guid}/end-turn")]
    public async Task<ActionResult> EndTurn(Guid id, [Required] string playerName)
    {
        CombatWithMetadata combat = await _combatsService.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Instance.EndTurn(side);

        await _combatsService.SaveCombat(combat);
        return NoContent();
    }

    static bool PlayerInCombat(CombatWithMetadata combat, [Required] string playerName)
    {
        return PlayerInCombat(combat, playerName, out _);
    }

    static bool PlayerInCombat(CombatWithMetadata combat, [Required] string playerName, out CombatSide side)
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
