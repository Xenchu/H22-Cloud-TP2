using Newtonsoft.Json;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;
using System.Text;

namespace RapidAuto.MVC.Services
{
    public class FavorisServiceProxy : IFavorisService
    {
        private readonly HttpClient _httpClient;
        private const string _favorisApiUrl = "api/favoris/";
        private readonly ILogger<FavorisServiceProxy> _logger;

        public FavorisServiceProxy(HttpClient httpClient, ILogger<FavorisServiceProxy> logger)
        {
            _httpClient = httpClient;

            _logger = logger;
        }

        public async Task<IEnumerable<Vehicule>> ObtenirFavoris()
        {
            var reponse = await _httpClient.GetAsync(_favorisApiUrl);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<List<Vehicule>>();
        }

        public async Task AjouterFavori(Vehicule vehicule)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(vehicule), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PostAsync(_favorisApiUrl, content);

            JournalisationErreurAPI(reponse);
        }

        public async Task SupprimerFavori(int idVehicule)
        {
            var reponse = await _httpClient.DeleteAsync(_favorisApiUrl + idVehicule);

            JournalisationErreurAPI(reponse);
        }

        public async Task<bool> FavoriExiste(int idVehicule)
        {
            var vehiculesFavoris = await ObtenirFavoris();

            return vehiculesFavoris.Any(v => v.Id == idVehicule);
        }

        private void JournalisationErreurAPI(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode <= 499)
            {
                _logger.LogError(CustomLogEvenements.LogCommandeService, "(Bad Request) - Mauvaise requête du côté de l'API !");
            }

            if ((int)httpResponseMessage.StatusCode >= 500 && (int)httpResponseMessage.StatusCode <= 599)
            {
                _logger.LogCritical(CustomLogEvenements.LogCommandeService, "(Critical Error) - Erreur grave du côté de l'API !");
            }
        }
    }
}
