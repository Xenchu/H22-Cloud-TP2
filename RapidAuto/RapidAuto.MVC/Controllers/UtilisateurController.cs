using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilisateurService _utilisateurProxy;
        private readonly ICommandeService _commandeProxy;

        public UtilisateurController(IConfiguration configuration, IUtilisateurService utilisateurProxy, ICommandeService commandeProxy)
        {
            _configuration = configuration;
            _utilisateurProxy = utilisateurProxy;
            _commandeProxy = commandeProxy;
        }

        // GET: UtilisateurController
        public async Task<ActionResult> Index()
        {
            var listeDesUtilisateurs = await _utilisateurProxy.ObtenirUtilisateurs();

            return View(listeDesUtilisateurs.ToList());
        }

        // GET: UtilisateurController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _utilisateurProxy.ObtenirUnUtilisateur(id.Value);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: UtilisateurController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilisateurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.IdentifiantUtilisateur = AttribuerIdentifiant(utilisateur);
                
                try
                {
                    await _utilisateurProxy.AjouterUtilisateur(utilisateur);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Un erreur est survenue lors de la création du compte utilisateur.");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(utilisateur);
        }

        // GET: UtilisateurController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _utilisateurProxy.ObtenirUnUtilisateur(id.Value);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: UtilisateurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Utilisateur utilisateur)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _utilisateurProxy.ModifierUtilisateur(utilisateur);
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Une erreur est survenue lors de la modification du compte utilisateur.");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(utilisateur);
        }

        // GET: UtilisateurController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _utilisateurProxy.ObtenirUnUtilisateur(id.Value);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: UtilisateurController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id, Utilisateur utilisateur)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != utilisateur.Id)
            {
                return NotFound();
            }

            // On supprime les commandes associées à l'utilisateur.
            var commandes = await _commandeProxy.ObtenirCommandes();
            var commandesDeLUtilisateur = commandes.Where(x => x.NumeroIdentifiantUtilisateur == utilisateur.IdentifiantUtilisateur).ToList();

            if (commandesDeLUtilisateur.Count > 0)
            {
                foreach (var commande in commandesDeLUtilisateur)
                {
                    await _commandeProxy.Supprimer(commande.Id);
                }
            }

            await _utilisateurProxy.SupprimerUtilisateur(id.Value);

            return RedirectToAction(nameof(Index));
        }

        private string AttribuerIdentifiant(Utilisateur utilisateur)
        {
            Random random = new Random();

            var identifiantUtilisateur = utilisateur.Prenom[0] + utilisateur.Nom.Substring(0, 3) + random.Next(100, 999).ToString();

            identifiantUtilisateur = identifiantUtilisateur.ToUpper();

            return identifiantUtilisateur;
        }
    }
}
