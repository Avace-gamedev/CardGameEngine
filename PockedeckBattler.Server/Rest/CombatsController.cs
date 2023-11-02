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
    readonly ICombatsInPreparationStore _combatsInPreparationStore;
    readonly ICombatsStore _combatsStore;

    public CombatsController(ICombatsStore combatsStore, ICombatsInPreparationStore combatsInPreparationStore)
    {
        _combatsStore = combatsStore;
        _combatsInPreparationStore = combatsInPreparationStore;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<CombatView> Get(Guid id, [Required] string playerName)
    {
        StoredCombat combat = _combatsStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet]
    public IEnumerable<PlayerCombatView> GetCombatsOfPlayer([Required] string playerName)
    {
        return _combatsStore.GetCombatsInvolvingPlayer(playerName).Select(c => c.PlayerView(c.LeftPlayerName == playerName ? CombatSide.Left : CombatSide.Right));
    }

    [HttpGet("in-preparation/{id:guid}")]
    public ActionResult<CombatInPreparationView> GetCombatInPreparation(Guid id, [Required] string playerName)
    {
        StoredCombatInPreparation combat = _combatsInPreparationStore.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        return combat.View();
    }

    [HttpGet("in-preparation")]
    public IEnumerable<CombatInPreparationView> GetCombatsInPreparationOfPlayer([Required] string playerName)
    {
        return _combatsInPreparationStore.GetCombatsInPreparationInvolvingPlayer(playerName).Select(c => c.View());
    }

    [HttpPost]
    public async Task<CombatInPreparationView> CreateCombat([Required] string playerName)
    {
        StoredCombatInPreparation combatInPreparation = new(Guid.NewGuid(), playerName);
        await _combatsInPreparationStore.SaveCombatInPreparation(combatInPreparation);

        return combatInPreparation.View();
    }

    [HttpPost("in-preparation/{id:guid}")]
    public async Task UpdateCombatInPreparation(Guid id, UpdateCombatInPreparationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PlayerName))
        {
            throw new Exception("Player name cannot be empty");
        }

        StoredCombatInPreparation combatInPreparation = _combatsInPreparationStore.RequireCombatInPreparation(id);

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

        await _combatsInPreparationStore.SaveCombatInPreparation(combatInPreparation);
    }

    [HttpPost("in-preparation/{id:guid}/leave")]
    public async Task<ActionResult> LeaveCombatInPreparation(Guid id, [Required] string playerName)
    {
        StoredCombatInPreparation combat = _combatsInPreparationStore.RequireCombatInPreparation(id);
        if (!PlayerInCombat(combat, playerName))
        {
            return NotFound();
        }

        if (playerName == combat.LeftPlayerName)
        {
            await _combatsInPreparationStore.DeleteCombatInPreparation(id);
        }
        else if (playerName == combat.RightPlayerName)
        {
            combat.RightPlayerName = null;
            combat.RightFrontCharacter = null;
            combat.RightBackCharacter = null;
            await _combatsInPreparationStore.SaveCombatInPreparation(combat);
        }
        else
        {
            throw new Exception($"Player {playerName} is not part of combat {id}");
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    public ActionResult<CombatView> StartCombat(Guid combatId, [Required] string playerName)
    {
        StoredCombatInPreparation inPreparation = _combatsInPreparationStore.RequireCombatInPreparation(combatId);
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
        _combatsStore.SaveCombat(combat);

        return combat.View();
    }

    [HttpPost("{id:guid}/{side}/play/{index:int}")]
    public ActionResult PlayCard(Guid id, [Required] string playerName, int index)
    {
        StoredCombat combat = _combatsStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Combat.PlayCardAt(side, index);

        _combatsStore.SaveCombat(combat);
        return NoContent();
    }

    [HttpPost("{id:guid}/{side}/end-turn")]
    public ActionResult EndTurn(Guid id, [Required] string playerName)
    {
        StoredCombat combat = _combatsStore.RequireCombat(id);
        if (!PlayerInCombat(combat, playerName, out CombatSide side))
        {
            return NotFound();
        }

        combat.Combat.EndSideTurnAndStartNextOne(side);

        _combatsStore.SaveCombat(combat);
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
