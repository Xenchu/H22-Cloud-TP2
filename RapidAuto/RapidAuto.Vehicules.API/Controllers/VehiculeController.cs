using Microsoft.AspNetCore.Mvc;
using RapidAuto.Vehicules.API.Interfaces;
using RapidAuto.Vehicules.API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidAuto.Vehicules.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculeController : ControllerBase
    {
        private readonly IVehiculesService _vehiculesService;

        public VehiculeController(IVehiculesService vehiculesService)
        {
            _vehiculesService = vehiculesService;
        }

        // GET: api/<VehiculeController>
        [HttpGet]
        public async Task<IEnumerable<Vehicule>> Get()
        {
            return await _vehiculesService.ObtenirVehicules();
        }

        // GET api/<VehiculeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicule>> Get(int id)
        {
            var vehicule = await _vehiculesService.ObtenirUnVehicule(id);

            if (vehicule is null)
            {
                return NotFound();
            }

            return vehicule;
        }

        [HttpGet]
        [Route("[action]/{modeleDuVehicule}")]
        public async Task<string> GetCodeUnique(string modeleDuVehicule)
        {
            var codeUnique = _vehiculesService.GenererCode(modeleDuVehicule);

            return codeUnique;
        }

        // POST api/<VehiculeController>
        [HttpPost]
        public async Task<IActionResult> Post(Vehicule vehicule)
        {
            await _vehiculesService.Ajouter(vehicule);

            return CreatedAtAction(nameof(Post), new { id = vehicule.Id }, vehicule);
        }

        // PUT api/<VehiculeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return BadRequest();
            }

            var vehiculeExistant = await _vehiculesService.ObtenirUnVehicule(id);

            if (vehiculeExistant is null)
            {
                return NotFound();
            }

            await _vehiculesService.Modifier(vehicule);

            return NoContent();
        }

        // DELETE api/<VehiculeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var vehicule = await _vehiculesService.ObtenirUnVehicule(id);

            if (vehicule is null)
            {
                return NotFound();
            }

            await _vehiculesService.Supprimer(id);

            return NoContent();
        }
    }
}
