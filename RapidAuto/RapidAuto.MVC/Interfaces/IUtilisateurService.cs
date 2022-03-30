using RapidAuto.MVC.Models;

namespace RapidAuto.MVC.Interfaces
{
    public interface IUtilisateurService
    {
        Task<IEnumerable<Utilisateur>> ObtenirUtilisateurs();
        Task<Utilisateur> ObtenirUnUtilisateur(int id);
        Task<Utilisateur> ObtenirUnUtilisateurAvecNumeroIdentifiant(string numeroIdentifiant);
        Task AjouterUtilisateur(Utilisateur utilisateur);
        Task ModifierUtilisateur(Utilisateur utilisateur);
        Task SupprimerUtilisateur(int id);
        Task<bool> UtilisateurExiste(int id);
    }
}
