using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Interfaces
{
    public interface IVehiculeService
    {
        Task<IEnumerable<Vehicule>> ObtenirVehicules();
        Task<Vehicule> ObtenirUnVehicule(int? id);
        Task<string> ObtenirCodeUnique(string modeleDuVehicule);
        Task Ajouter(Vehicule vehicule);
        Task Modifier(Vehicule vehicule);
        Task Supprimer(int id);
    }
}
