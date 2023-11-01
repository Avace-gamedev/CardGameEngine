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
    public CombatView Get(Guid id)
    {
        return CombatStore.RequireCombat(id).View();
    }

    [HttpGet]
    public IEnumerable<PlayerCombatView> GetCombatsOfPlayer(string playerName)
    {
        return CombatStore.GetCombatsInvolvingPlayer(playerName).Select(c => c.PlayerView(c.LeftPlayerName == playerName ? CombatSide.Left : CombatSide.Right));
    }

    [HttpGet("in-preparation/{id:guid}")]
    public CombatInPreparationView GetCombatInPreparation(Guid id)
    {
        return CombatStore.RequireCombatInPreparation(id).View();
    }

    [HttpGet("in-preparation")]
    public IEnumerable<CombatInPreparationView> GetCombatsInPreparationOfPlayer(string playerName)
    {
        return CombatStore.GetCombatsInPreparationInvolvingPlayer(playerName).Select(c => c.View());
    }

    [HttpPost]
    public CombatInPreparationView CreateCombat(string playerName)
    {
        StoredCombatInPreparation combatInPreparation = new(Guid.NewGuid(), playerName);
        CombatStore.SaveCombatInPreparation(combatInPreparation);

        return combatInPreparation.View();
    }

    [HttpPost("in-preparation/{id:guid}")]
    public void UpdateCombatInPreparation(Guid id, string playerName, UpdateCombatInPreparationRequest request)
    {
        StoredCombatInPreparation combatInPreparation = CombatStore.RequireCombatInPreparation(id);

        if (playerName == combatInPreparation.LeftPlayerName)
        {
            if (request.PlayerName != playerName)
            {
                throw new Exception("Bad player name in request");
            }

            combatInPreparation.LeftFrontCharacter = request.FrontCharacter;
            combatInPreparation.LeftBackCharacter = request.BackCharacter;
        }
        else if (playerName == combatInPreparation.RightPlayerName)
        {
            combatInPreparation.RightPlayerName = request.PlayerName;
            combatInPreparation.RightFrontCharacter = request.FrontCharacter;
            combatInPreparation.RightBackCharacter = request.BackCharacter;
        }
        else
        {
            throw new Exception($"Player {playerName} is not part of combat {id}");
        }

        CombatStore.SaveCombatInPreparation(combatInPreparation);
    }

    [HttpPost("in-preparation/{id:guid}/leave")]
    public void LeaveCombatInPreparation(Guid id, string playerName)
    {
        StoredCombatInPreparation combatInPreparation = CombatStore.RequireCombatInPreparation(id);

        if (playerName == combatInPreparation.LeftPlayerName)
        {
            CombatStore.DeleteCombatInPreparation(id);
        }
        else if (playerName == combatInPreparation.RightPlayerName)
        {
            combatInPreparation.RightPlayerName = null;
            combatInPreparation.RightFrontCharacter = null;
            combatInPreparation.RightBackCharacter = null;
        }
        else
        {
            throw new Exception($"Player {playerName} is not part of combat {id}");
        }

        CombatStore.SaveCombatInPreparation(combatInPreparation);
    }

    [HttpPost("{id:guid}/start")]
    public CombatView StartCombat(Guid combatId)
    {
        StoredCombatInPreparation inPreparation = CombatStore.RequireCombatInPreparation(combatId);

        if (inPreparation.LeftFrontCharacter == null && inPreparation.LeftBackCharacter == null)
        {
            throw new InvalidCombatConfigurationException("Left side doesn't have any character");
        }

        if (inPreparation.RightPlayerName == null)
        {
            throw new InvalidCombatConfigurationException("Right side not configured");
        }

        if (inPreparation.RightFrontCharacter == null && inPreparation.RightBackCharacter == null)
        {
            throw new InvalidCombatConfigurationException("Right side doesn't have any character");
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
    public void PlayCard(Guid id, CombatSide side, int index)
    {
        StoredCombat combat = CombatStore.RequireCombat(id);
        combat.Combat.PlayCardAt(side, index);

        CombatStore.SaveCombat(combat);
    }

    [HttpPost("{id:guid}/{side}/end-turn")]
    public void EndTurn(Guid id, CombatSide side)
    {
        StoredCombat combat = CombatStore.RequireCombat(id);
        combat.Combat.EndSideTurnAndStartNextOne(side);

        CombatStore.SaveCombat(combat);
    }
}
