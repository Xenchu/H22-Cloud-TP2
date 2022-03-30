using RapidAuto.Commandes.API.Interfaces;
using RapidAuto.Commandes.API.Model;

namespace RapidAuto.Commandes.API.Services
{
    public class CommandesService : ICommandesService
    {
        protected readonly IAsyncRepository _commandesRepository;

        public CommandesService(IAsyncRepository commandesRepository)
        {
            _commandesRepository = commandesRepository;
        }

        public async Task Ajouter(Commande commande)
        {
            await _commandesRepository.AddAsync(commande);
        }

        public async Task Modifier(Commande commande)
        {
            await _commandesRepository.EditAsync(commande);
        }

        public async Task<IEnumerable<Commande>> ObtenirCommandes()
        {
            return await _commandesRepository.ListAsync();
        }

        public async Task<Commande> ObtenirUneCommande(int id)
        {
            return await _commandesRepository.GetByIdAsync(id);
        }

        public async Task Supprimer(int id)
        {
            var commande = await _commandesRepository.GetByIdAsync(id);
            await _commandesRepository.DeleteAsync(commande);
        }
    }
}
