using RapidAuto.Commandes.API.Model;

namespace RapidAuto.Commandes.API.Interfaces
{
    public interface ICommandesService
    {
        Task<IEnumerable<Commande>> ObtenirCommandes();
        Task<Commande> ObtenirUneCommande(int id);
        Task Ajouter(Commande commande);
        Task Modifier(Commande commande);
        Task Supprimer(int id);
    }
}
