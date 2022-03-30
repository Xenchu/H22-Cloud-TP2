using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Interfaces
{
    public interface ICommandeService
    {
        Task<IEnumerable<Commande>> ObtenirCommandes();
        Task<Commande> ObtenirUneCommande(int? id);
        Task Ajouter(Commande commande);
        Task Modifier(Commande commande);
        Task Supprimer(int id);
    }
}
