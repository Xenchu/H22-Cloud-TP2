using RapidAuto.Vehicules.API.Model;

namespace RapidAuto.Vehicules.API.Interfaces
{
    public interface IVehiculesService
    {
        Task<IEnumerable<Vehicule>> ObtenirVehicules();
        Task<Vehicule> ObtenirUnVehicule(int id);
        Task Ajouter(Vehicule vehicule);
        Task Modifier(Vehicule vehicule);
        Task Supprimer(int id);
        string GenererCode(string modeleDuVehicule);
    }
}
