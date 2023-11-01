using CardGame.Engine.Characters;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.Content.Characters;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.Controllers;

[ApiController]
[Route("characters")]
public class CharactersController : ControllerBase
{
    [HttpGet]
    public IEnumerable<CharacterView> GetAll()
    {
        return Characters.All.Select(c => c.View());
    }

    [HttpGet("{name}")]
    public ActionResult<CharacterView> Get(string name)
    {
        Character? character = Characters.GetByName(name);
        if (character == null)
        {
            return NotFound();
        }

        return character.View();
    }
}
