#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidAuto.Utilisateurs.API.Data;
using RapidAuto.Utilisateurs.API.Interfaces;
using RapidAuto.Utilisateurs.API.Model;

namespace RapidAuto.Utilisateurs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IUtilisateursService _utilisateursService;

        public UtilisateursController(IUtilisateursService utilisateursService)
        {
            _utilisateursService = utilisateursService;
        }

        // GET: api/<UtilisateursController>
        [HttpGet]
        public async Task<IEnumerable<Utilisateur>> GetUtilisateurs()
        {
            return await _utilisateursService.ObtenirUtilisateurs();
        }

        // GET: api/<UtilisateursController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _utilisateursService.ObtenirUnUtilisateur(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // GET: api/<UtilisateursController>/GetUtilisateurAvecNumeroIdentifiant/FLAG280
        [HttpGet]
        [Route("[action]/{numeroIdentifiant}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurAvecNumeroIdentifiant(string numeroIdentifiant)
        {
            var utilisateur = await _utilisateursService.ObtenirUnUtilisateurAvecNumeroIdentifiant(numeroIdentifiant);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // POST: api/<UtilisateursController>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUtilisateur(Utilisateur utilisateur)
        {
            await _utilisateursService.AjouterUtilisateur(utilisateur);

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // PUT: api/<UtilisateursController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, [FromBody] Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }

            var utilisateurExistant = await _utilisateursService.ObtenirUnUtilisateur(id);

            if (utilisateurExistant == null)
            {
                return NotFound();
            }

            await _utilisateursService.ModifierUtilisateur(utilisateur);

            return NoContent();
        }

        // DELETE: api/<UtilisateursController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _utilisateursService.ObtenirUnUtilisateur(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            await _utilisateursService.SupprimerUtilisateur(id);

            return NoContent();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<bool> UtilisateurExists(int id)
        {
            return await _utilisateursService.UtilisateurExiste(id);
        }
    }
}
