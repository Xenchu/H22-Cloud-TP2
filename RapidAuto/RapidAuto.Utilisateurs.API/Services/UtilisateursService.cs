using RapidAuto.Utilisateurs.API.Interfaces;
using RapidAuto.Utilisateurs.API.Model;

namespace RapidAuto.Utilisateurs.API.Services
{
    public class UtilisateursService : IUtilisateursService
    {
        private readonly IAsyncRepository _utilisateursRepository;

        public UtilisateursService(IAsyncRepository utilisateursRepository)
        {
            _utilisateursRepository = utilisateursRepository;
        }

        public async Task AjouterUtilisateur(Utilisateur utilisateur)
        {
            await _utilisateursRepository.AddAsync(utilisateur);
        }

        public async Task ModifierUtilisateur(Utilisateur utilisateur)
        {
            await _utilisateursRepository.EditAsync(utilisateur);
        }

        public async Task<Utilisateur> ObtenirUnUtilisateur(int id)
        {
            return await _utilisateursRepository.GetByIdAsync(id);
        }

        public async Task<Utilisateur> ObtenirUnUtilisateurAvecNumeroIdentifiant(string numeroIdentifiant)
        {
            var utilisateurs = await _utilisateursRepository.ListAsync();

            return utilisateurs.FirstOrDefault(u => u.IdentifiantUtilisateur == numeroIdentifiant);
        }

        public async Task<IEnumerable<Utilisateur>> ObtenirUtilisateurs()
        {
            return await _utilisateursRepository.ListAsync();
        }

        public async Task SupprimerUtilisateur(int id)
        {
            var utilisateur = await _utilisateursRepository.GetByIdAsync(id);
            await _utilisateursRepository.DeleteAsync(utilisateur);
        }

        public async Task<bool> UtilisateurExiste(int id)
        {
            var utilisateurs = await _utilisateursRepository.ListAsync();

            return utilisateurs.Any(u => u.Id == id);
        }
    }
}
