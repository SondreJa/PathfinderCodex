using Domain.Handlers;
using Domain.Models.Spells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SpellsController
    {
        private readonly ISpellHandler handler;

        public SpellsController(ISpellHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Spell>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSpells()
        {
            var data = await handler.GetAllSpells();
            return new OkObjectResult(data);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Spell), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSpell(string name)
        {
            var data = await handler.GetSpell(name);
            if (data == null)
                return new StatusCodeResult(204);
            return new OkObjectResult(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSpell([FromBody]Spell spell)
        {
            await handler.AddSpell(spell);
            return new StatusCodeResult(204);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveSpell(string name)
        {
            await handler.RemoveSpell(name);
            return new StatusCodeResult(204);
        }
    }
}
