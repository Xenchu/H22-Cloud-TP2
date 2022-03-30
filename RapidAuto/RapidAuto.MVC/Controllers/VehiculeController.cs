using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;
using System.Linq;

namespace RapidAuto.MVC.Controllers
{
    public class VehiculeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IVehiculeService _vehiculeProxy;
        private readonly IFichierService _fichierProxy;
        private readonly IFavorisService _favorisProxy;
        private readonly ILogger<VehiculeController> _logger;


        public VehiculeController(IConfiguration configuration, IVehiculeService vehiculeProxy, IFichierService fichierProxy, IFavorisService favorisService, ILogger<VehiculeController> logger)
        {
            _configuration = configuration;
            _vehiculeProxy = vehiculeProxy;
            _fichierProxy = fichierProxy;
            _favorisProxy = favorisService;
            _logger = logger;
        }

        // GET: VehiculeController
        public async Task<ActionResult> Index(string ordreDeTri, string searchString)
        {
            var nomConteneur = "images";
            var urlConteneur = await _fichierProxy.ObtenirUrlConteneur(nomConteneur);
            ViewBag.UrlConteneur = urlConteneur;

            ViewData["TriParPrix"] = "prix_desc";
            if (ordreDeTri == "prix_desc")
            {
                ViewData["TriParPrix"] = "prix_asc";
            }
            else if (ordreDeTri == "prix_asc")
            {
                ViewData["TriParPrix"] = "prix_desc";
            }

            var listeDeVehicules = await _vehiculeProxy.ObtenirVehicules();

            switch (ordreDeTri)
            {
                case "prix_desc":
                    listeDeVehicules = listeDeVehicules.OrderByDescending(x => x.Prix);
                    break;
                case "prix_asc":
                    listeDeVehicules = listeDeVehicules.OrderBy(x => x.Prix);
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                listeDeVehicules = listeDeVehicules.Where(v => v.Constructeur.ToLower().Contains(searchString.ToLower())
                                                            || v.Modele.ToLower().Contains(searchString.ToLower()));

                _logger.LogInformation(CustomLogEvenements.Recherche, $"Recherche effectuée ({searchString}), sur la liste des véhicules.");
            }

            return View(listeDeVehicules.ToList());
        }

        // GET: VehiculeController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var nomConteneur = "images";
            var urlConteneur = await _fichierProxy.ObtenirUrlConteneur(nomConteneur);
            ViewBag.UrlConteneur = urlConteneur;

            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _vehiculeProxy.ObtenirUnVehicule(id);

            if (vehicule == null)
            {
                return NotFound();
            }

            _logger.LogInformation(CustomLogEvenements.LectureDUnVehicule, $"Affichage du véhicule (Id: {vehicule.Id})");

            return View(vehicule);
        }

        // GET: VehiculeController/Create
        public ActionResult Create()
        {
            ViewBag.TypesDeVehicule = RecupererTypesDeVehicule();

            return View();
        }

        // POST: VehiculeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vehicule vehicule)
        {
            ViewBag.TypesDeVehicule = RecupererTypesDeVehicule();

            var images = new List<IFormFile>() { vehicule.Image1, vehicule.Image2 };

            if (ValiderExtensionFichier(vehicule.Image1.FileName) == false)
            {
                ModelState.AddModelError("Image1", "Attention, seulement les extensions jpg, jpeg et png sont supportées !");
            }

            if (ValiderExtensionFichier(vehicule.Image2.FileName) == false)
            {
                ModelState.AddModelError("Image2", "Attention, seulement les extensions jpg, jpeg et png sont supportées !");
            }

            if (vehicule.Image1 == null)
            {
                ModelState.AddModelError("Image1", "Attention, le champ est obligatoire !");
            }

            if (vehicule.Image2 == null)
            {
                ModelState.AddModelError("Image2", "Attention, le champ est obligatoire !");
            }

            if (ModelState.IsValid)
            {

                vehicule.CodeUnique = await _vehiculeProxy.ObtenirCodeUnique(vehicule.Modele);
                var imagesConverties = await _fichierProxy.ConvertirImagesEnBytes(images);

                await _fichierProxy.AjouterImages(imagesConverties, vehicule.CodeUnique);
                var nomsDesImagesDuVehicule = await _fichierProxy.ObtenirNomsImages(vehicule.CodeUnique);

                nomsDesImagesDuVehicule.Sort();

                vehicule.NomImage1 = nomsDesImagesDuVehicule[0];
                vehicule.NomImage2 = nomsDesImagesDuVehicule[1];

                await _vehiculeProxy.Ajouter(vehicule);

                _logger.LogInformation(CustomLogEvenements.EnregistrementVehicule, $"Ajout d'un véhicule (Id: {vehicule.Id})");

                return RedirectToAction(nameof(Index));
            }

            return View(vehicule);
        }

        // GET: VehiculeController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.TypesDeVehicule = RecupererTypesDeVehicule();

            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _vehiculeProxy.ObtenirUnVehicule(id.Value);

            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: VehiculeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Vehicule vehicule)
        {
            ViewBag.TypesDeVehicule = RecupererTypesDeVehicule();

            if (id == null)
            {
                return NotFound();
            }

            var vehiculeAvantModification = await _vehiculeProxy.ObtenirUnVehicule(vehicule.Id);

            if (vehiculeAvantModification.Modele != vehicule.Modele)
            {
                vehicule.CodeUnique = await _vehiculeProxy.ObtenirCodeUnique(vehicule.Modele);

                // Si le modèle du véhicule change, on doit modifier le nom des images avec le nouveau code unique attribué à celui-ci.
                // **** On fait ce traitement si et seulement si on n'ajoute PAS de nouvelles images ****

                if (vehicule.Image1 == null)
                {
                    vehicule.NomImage1 = await _fichierProxy.ModifierImage(new List<string>() { vehicule.NomImage1, vehicule.CodeUnique });
                }

                if (vehicule.Image2 == null)
                {
                    vehicule.NomImage2 = await _fichierProxy.ModifierImage(new List<string>() { vehicule.NomImage2, vehicule.CodeUnique });
                }
            }

            var listeNouvellesImages = new List<IFormFile>();

            if (vehicule.Image1 != null)
            {
                if (ValiderExtensionFichier(vehicule.Image1.FileName) == false)
                {
                    ModelState.AddModelError("Image1", "Attention, seulement les extensions jpg, jpeg et png sont supportées !");
                }
                else
                {
                    listeNouvellesImages.Add(vehicule.Image1);
                    // On supprime l'ancienne Image1
                    await _fichierProxy.SupprimerImage(vehicule.NomImage1);
                }
            }

            if (vehicule.Image2 != null)
            {
                if (ValiderExtensionFichier(vehicule.Image1.FileName) == false)
                {
                    ModelState.AddModelError("Image2", "Attention, seulement les extensions jpg, jpeg et png sont supportées !");
                }
                else
                {
                    listeNouvellesImages.Add(vehicule.Image2);
                    // On supprime l'ancienne Image2
                    await _fichierProxy.SupprimerImage(vehicule.NomImage2);
                }
            }

            // Pour la logique de l'enregistrement des images, on s'assure qu'on enregistre les deux.
            if (listeNouvellesImages.Count == 1)
            {
                ModelState.AddModelError("Image1", "Attention, si vous ajoutez de nouvelles images, vous devez le faire pour l'Image 1 et l'Image 2 ");
                ModelState.AddModelError("Image2", "Attention, si vous ajoutez de nouvelles images, vous devez le faire pour l'Image 1 et l'Image 2 ");
            }


            if (ModelState.IsValid)
            {
                if (listeNouvellesImages.Count > 0)
                {
                    var imagesConverties = await _fichierProxy.ConvertirImagesEnBytes(listeNouvellesImages);
                    await _fichierProxy.AjouterImages(imagesConverties, vehicule.CodeUnique);

                    var nomsDesImagesDuVehicule = await _fichierProxy.ObtenirNomsImages(vehicule.CodeUnique);

                    nomsDesImagesDuVehicule.Sort();

                    vehicule.NomImage1 = nomsDesImagesDuVehicule[0];
                    vehicule.NomImage2 = nomsDesImagesDuVehicule[1];
                }

                await _vehiculeProxy.Modifier(vehicule);

                _logger.LogInformation(CustomLogEvenements.ModificationVehicule, $"Modification d'un véhicule (Id: {vehicule.Id})");

                return RedirectToAction(nameof(Index));
            }

            return View(vehicule);
        }

        // GET: VehiculeController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _vehiculeProxy.ObtenirUnVehicule(id.Value);

            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: VehiculeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var vehicule = await _vehiculeProxy.ObtenirUnVehicule(id);

            if (vehicule == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _vehiculeProxy.Supprimer(id);
            // On supprime les images reliées au véhicule.
            await _fichierProxy.SupprimerImage(vehicule.NomImage1);
            await _fichierProxy.SupprimerImage(vehicule.NomImage2);

            // On supprime le favori relié au véhicule s'il existe.
            if (await _favorisProxy.FavoriExiste(vehicule.Id))
            {
                await _favorisProxy.SupprimerFavori(vehicule.Id);
            }

            _logger.LogInformation(CustomLogEvenements.SuppressionVehicule, $"Suppression d'un véhicule (Id:{vehicule.Id})");

            return RedirectToAction(nameof(Index));
        }

        private List<string> RecupererTypesDeVehicule()
        {
            return _configuration.GetSection("Vehicule:TypeDeVehicule").Get<string[]>().ToList();
        }

        private bool ValiderExtensionFichier(string nomDuFichierEtExtension)
        {
            if (!nomDuFichierEtExtension.EndsWith(".png") && !nomDuFichierEtExtension.EndsWith(".jpg") && !nomDuFichierEtExtension.EndsWith(".jpeg"))
            {
                return false;
            }

            return true;
        }
    }
}
