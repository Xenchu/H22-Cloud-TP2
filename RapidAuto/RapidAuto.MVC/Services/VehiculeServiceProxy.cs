using Newtonsoft.Json;
using RapidAuto.MVC.Models;
using RapidAuto.MVC.Interfaces;
using System.Text;

namespace RapidAuto.MVC.Services
{
    public class VehiculeServiceProxy : IVehiculeService
    {
        private readonly HttpClient _httpClient;
        private const string _vehiculeApiUrl = "api/Vehicule/";
        private readonly ILogger<VehiculeServiceProxy> _logger;

        public VehiculeServiceProxy(HttpClient httpClient, ILogger<VehiculeServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Vehicule>> ObtenirVehicules()
        {
            var reponse = await _httpClient.GetAsync(_vehiculeApiUrl);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<List<Vehicule>>();
        }

        public async Task<Vehicule> ObtenirUnVehicule(int? id)
        {
            var reponse = await _httpClient.GetAsync(_vehiculeApiUrl + id);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<Vehicule>();
        }

        public async Task Ajouter(Vehicule vehicule)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(vehicule), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_vehiculeApiUrl, content);

            JournalisationErreurAPI(response);
        }

        public async Task Modifier(Vehicule vehicule)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(vehicule), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_vehiculeApiUrl + vehicule.Id, content);

            JournalisationErreurAPI(result);

            await _httpClient.PutAsync(_vehiculeApiUrl + vehicule.Id, content);
        }

        public async Task Supprimer(int id)
        {
            var reponse = await _httpClient.DeleteAsync(_vehiculeApiUrl + id);

            JournalisationErreurAPI(reponse);
        }

        public async Task<string> ObtenirCodeUnique(string modeleDuVehicule)
        {
            var reponse = await _httpClient.GetAsync(_vehiculeApiUrl + "GetCodeUnique/" + modeleDuVehicule);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadAsStringAsync();
        }

        private void JournalisationErreurAPI(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode <= 499)
            {
                _logger.LogError(CustomLogEvenements.LogVehiculeService, "(Bad Request) - Mauvaise requête du côté de l'API !");
            }

            if ((int)httpResponseMessage.StatusCode >= 500 && (int)httpResponseMessage.StatusCode <= 599)
            {
                _logger.LogCritical(CustomLogEvenements.LogVehiculeService, "(Critical Error) - Erreur grave du côté de l'API !");
            }
        }
    }
}
