using Microsoft.AspNetCore.Mvc;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Controllers
{
    public class FavorisController : Controller
    {
        private readonly IFavorisService _favorisService;
        private readonly IVehiculeService _vehiculeService;

        public FavorisController(IFavorisService favorisService, IVehiculeService vehiculeService)
        {
            _favorisService = favorisService;
            _vehiculeService = vehiculeService;
        }

        // GET: FavorisController
        public async Task<ActionResult> Index()
        {
            var listeDesFavoris = await _favorisService.ObtenirFavoris();

            return View(listeDesFavoris.ToList());
        }

        // GET: FavorisController/Create
        public async Task<ActionResult> Create(int idVehicule)
        {
            var vehicule = await _vehiculeService.ObtenirUnVehicule(idVehicule);

            if (vehicule == null)
            {
                return View("Error");
            }

            return View(vehicule);
        }

        // POST: FavorisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vehicule vehicule)
        {
            await _favorisService.AjouterFavori(vehicule);

            return RedirectToAction(nameof(Index));
        }

        // GET: FavorisController/Delete/5
        public async Task<ActionResult> Delete(int idVehicule)
        {
            var vehiculeFavori = await _vehiculeService.ObtenirUnVehicule(idVehicule);

            if (vehiculeFavori == null)
            {
                return View("Error");
            }

            return View(vehiculeFavori);
        }

        // POST: FavorisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Vehicule vehicule)
        {
            await _favorisService.SupprimerFavori(vehicule.Id);

            return RedirectToAction(nameof(Index));
        }

    }
}
