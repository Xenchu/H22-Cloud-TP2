using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Controllers
{
    public class CommandeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICommandeService _commandeProxy;
        private readonly IUtilisateurService _utilisateurProxy;
        private readonly ILogger<CommandeController> _logger;
        private readonly IVehiculeService _vehiculeProxy;

        public CommandeController(IConfiguration config, ICommandeService commandeProxy, IUtilisateurService utilisateurProxy, ILogger<CommandeController> logger, IVehiculeService vehiculeProxy)
        {
            _config = config;
            _commandeProxy = commandeProxy;
            _utilisateurProxy = utilisateurProxy;
            _logger = logger;
            _vehiculeProxy = vehiculeProxy;
        }

        // GET: CommandeController
        public async Task<ActionResult> Index()
        {
            var listeDesCommandes = await _commandeProxy.ObtenirCommandes();

            return View(listeDesCommandes.ToList());
        }

        // GET: CommandeController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _commandeProxy.ObtenirUneCommande(id);

            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // GET: CommandeController/Create
        public async Task<ActionResult> Create(int idVehicule)
        {
            ViewData["VehiculeACommander"] = await _vehiculeProxy.ObtenirUnVehicule(idVehicule);
            return View();
        }

        // POST: CommandeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Commande commande)
        {
            ViewData["VehiculeACommander"] = await _vehiculeProxy.ObtenirUnVehicule(commande.IdVehicule);
            var utilisateur = await _utilisateurProxy.ObtenirUnUtilisateurAvecNumeroIdentifiant(commande.NumeroIdentifiantUtilisateur); 

            if (utilisateur.IdentifiantUtilisateur == null) 
            {
                ModelState.AddModelError("NumeroIdentifiantUtilisateur", "Utilisateur inexistant, veuillez entrer un identifiant valide !");
                return View(commande);
            }

            if (ModelState.IsValid)
            {
                await _commandeProxy.Ajouter(commande);

                _logger.LogInformation(CustomLogEvenements.EnregistrementCommande, $"Ajout d'une commande (Id: {commande.Id})");

                var vehicule = await _vehiculeProxy.ObtenirUnVehicule(commande.IdVehicule);
                vehicule.EstDisponible = false;

                await _vehiculeProxy.Modifier(vehicule);

                return View("CommandeSucces", commande);
            }

            return View(commande);
        }

        // GET: CommandeController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _commandeProxy.ObtenirUneCommande(id.Value);

            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: CommandeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Commande commande)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != commande.Id)
            {
                return NotFound();
            }

            var utilisateur = await _utilisateurProxy.ObtenirUnUtilisateurAvecNumeroIdentifiant(commande.NumeroIdentifiantUtilisateur);

            if (utilisateur.IdentifiantUtilisateur == null)
            {
                ModelState.AddModelError("NumeroIdentifiantUtilisateur", "Utilisateur inexistant, veuillez entrer un identifiant valide !");
                return View(commande);
            }

            if (ModelState.IsValid)
            {
                await _commandeProxy.Modifier(commande);
                return RedirectToAction(nameof(Index));
            }

            return View(commande);
        }

        // GET: CommandeController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _commandeProxy.ObtenirUneCommande(id.Value);

            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: CommandeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var commande = await _commandeProxy.ObtenirUneCommande(id);

            if (commande == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _commandeProxy.Supprimer(id);

            // on remet le vehicule disponible
            var vehicule = await _vehiculeProxy.ObtenirUnVehicule(commande.IdVehicule);
            vehicule.EstDisponible = true;

            await _vehiculeProxy.Modifier(vehicule);

            return RedirectToAction(nameof(Index));
        }
    }
}
