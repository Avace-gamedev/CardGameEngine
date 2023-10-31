using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.Content.Characters;
using PockedeckBattler.Server.Stores;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.Controllers;

[ApiController]
[Route("combats")]
public class CombatsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<CombatView> GetAll()
    {
        return CombatStore.All.Select(c => c.View());
    }

    [HttpGet("{id:guid}")]
    public CombatView Get(Guid id)
    {
        return CombatStore.Require(id).View();
    }

    [HttpGet("{id:guid}/{side}")]
    public PlayerCombatView GetPlayerView(Guid id, CombatSide side)
    {
        return CombatStore.Require(id).PlayerView(side);
    }

    [HttpPost]
    public PlayerCombatView CreateCombat(CreateCombatRequest request)
    {
        IEnumerable<Character> playerTeam = request.PlayerTeam.Select(Characters.RequireByName);
        IEnumerable<Character> opponentTeam = request.OpponentTeam.Select(Characters.RequireByName);

        CombatInstance combat = new(playerTeam.ToList(), opponentTeam.ToList(), new CombatOptions());
        CombatStore.Register(combat);

        return combat.PlayerView(CombatSide.Left);
    }

    [HttpPost("{id:guid}/{side}/play/{index:int}")]
    public void PlayCard(Guid id, CombatSide side, int index)
    {
        CombatInstance combat = CombatStore.Require(id);
        combat.PlayCardAt(side, index);
    }

    [HttpPost("{id:guid}/{side}/end-turn")]
    public void EndTurn(Guid id, CombatSide side)
    {
        CombatInstance combat = CombatStore.Require(id);
        combat.EndCurrentSideTurnAndStartNextOne();
    }
}

public class CreateCombatRequest
{
    public string[] OpponentTeam { get; set; } = Array.Empty<string>();
    public string[] PlayerTeam { get; set; } = Array.Empty<string>();
}
