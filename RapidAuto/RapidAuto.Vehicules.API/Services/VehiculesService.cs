using RapidAuto.Vehicules.API.Interfaces;
using RapidAuto.Vehicules.API.Model;

namespace RapidAuto.Vehicules.API.Services
{
    public class VehiculesService : IVehiculesService
    {
        private readonly IAsyncRepository _vehiculesRepository;

        public VehiculesService(IAsyncRepository vehiculesRepository)
        {
            _vehiculesRepository = vehiculesRepository;
        }

        public async Task Ajouter(Vehicule vehicule)
        {
            await _vehiculesRepository.AddAsync(vehicule);
        }

        public string GenererCode(string modeleDuVehicule)
        {
            Random random = new Random();

            var codeUnique = modeleDuVehicule + random.Next(1000, 9999).ToString();

            return codeUnique;
        }

        public async Task Modifier(Vehicule vehicule)
        {
            await _vehiculesRepository.EditAsync(vehicule);
        }

        public async Task<Vehicule> ObtenirUnVehicule(int id)
        {
            return await _vehiculesRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Vehicule>> ObtenirVehicules()
        {
            return await _vehiculesRepository.ListAsync();
        }

        public async Task Supprimer(int id)
        {
            var vehicule = await _vehiculesRepository.GetByIdAsync(id);
            await _vehiculesRepository.DeleteAsync(vehicule);
        }
    }
}
