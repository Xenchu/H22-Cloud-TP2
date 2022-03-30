using Newtonsoft.Json;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;
using System.Text;

namespace RapidAuto.MVC.Services
{
    public class CommandeServiceProxy : ICommandeService
    {
        private readonly HttpClient _httpClient;
        private const string _commandeApiUrl = "api/Commande/";
        private readonly ILogger<CommandeServiceProxy> _logger;

        public CommandeServiceProxy(HttpClient httpClient, ILogger<CommandeServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Commande>> ObtenirCommandes()
        {
            var reponse = await _httpClient.GetAsync(_commandeApiUrl);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<List<Commande>>();
        }

        public async Task<Commande> ObtenirUneCommande(int? id)
        {
            var reponse = await _httpClient.GetAsync(_commandeApiUrl + id);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<Commande>();
        }

        public async Task Ajouter(Commande commande)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(commande), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PostAsync(_commandeApiUrl, content);

            JournalisationErreurAPI(reponse);
        }

        public async Task Modifier(Commande commande)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(commande), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PutAsync(_commandeApiUrl + commande.Id, content);

            JournalisationErreurAPI(reponse);

            await _httpClient.PutAsync(_commandeApiUrl + commande.Id, content);
        }

        public async Task Supprimer(int id)
        {
            var reponse = await _httpClient.DeleteAsync(_commandeApiUrl + id);

            JournalisationErreurAPI(reponse);
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
