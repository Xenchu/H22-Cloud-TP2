using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Interfaces
{
    public interface IFavorisService
    {
        Task<IEnumerable<Vehicule>> ObtenirFavoris();
        Task AjouterFavori(Vehicule vehicule);
        Task SupprimerFavori(int idVehicule);
        Task<bool> FavoriExiste(int idVehicule);
    }
}
