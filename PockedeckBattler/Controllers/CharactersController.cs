using CardGame.Engine.Characters;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Content.Characters;
using PockedeckBattler.Views;

namespace PockedeckBattler.Controllers;

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
    public ActionResult<CharacterView> GetAll(string name)
    {
        Character? character = Characters.GetByName(name);
        if (character == null)
        {
            return NotFound();
        }

        return character.View();
    }
}
