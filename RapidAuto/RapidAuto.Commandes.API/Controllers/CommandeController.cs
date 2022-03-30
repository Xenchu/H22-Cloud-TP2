using Microsoft.AspNetCore.Mvc;
using RapidAuto.Commandes.API.Interfaces;
using RapidAuto.Commandes.API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidAuto.Commandes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        protected readonly ICommandesService _commandesService;

        public CommandeController(ICommandesService commandesService)
        {
            _commandesService = commandesService;
        }

        // GET: api/<CommandeController>
        [HttpGet]
        public async Task<IEnumerable<Commande>> Get()
        {
            return await _commandesService.ObtenirCommandes();
        }

        // GET api/<CommandeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> Get(int id)
        {
            var commande = await _commandesService.ObtenirUneCommande(id);

            if (commande == null)
            {
                return NotFound();
            }

            return commande;
        }

        // POST api/<CommandeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Commande commande)
        {
            await _commandesService.Ajouter(commande);

            return CreatedAtAction(nameof(Post), new { id = commande.Id }, commande);
        }

        // PUT api/<CommandeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Commande commande)
        {
            if (id != commande.Id)
            {
                return BadRequest();
            }

            var commandeExistante = _commandesService.ObtenirUneCommande(id);

            if (commandeExistante is null)
            {
                return NotFound();
            }

            await _commandesService.Modifier(commande);

            return NoContent();
        }

        // DELETE api/<CommandeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var commande = _commandesService.ObtenirUneCommande(id);

            if (commande is null)
            {
                return NotFound();
            }

            await _commandesService.Supprimer(id);

            return NoContent();
        }
    }
}
