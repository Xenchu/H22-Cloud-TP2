using RapidAuto.Utilisateurs.API.Model;

namespace RapidAuto.Utilisateurs.API.Interfaces
{
    public interface IUtilisateursService
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
