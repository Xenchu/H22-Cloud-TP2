using Newtonsoft.Json;
using RapidAuto.MVC.Models;
using RapidAuto.MVC.Interfaces;
using System.Text;

namespace RapidAuto.MVC.Services
{
    public class UtilisateurServiceProxy : IUtilisateurService
    {
        private readonly HttpClient _httpClient;
        private const string _utilisateurApiUrl = "api/utilisateurs/";
        private readonly ILogger<UtilisateurServiceProxy> _logger;

        public UtilisateurServiceProxy(HttpClient httpClient, ILogger<UtilisateurServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Utilisateur>> ObtenirUtilisateurs()
        {
            var reponse = await _httpClient.GetAsync(_utilisateurApiUrl);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<List<Utilisateur>>();
        }

        public async Task<Utilisateur> ObtenirUnUtilisateur(int id)
        {
            var reponse = await _httpClient.GetAsync(_utilisateurApiUrl + id);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<Utilisateur>();
        }

        public async Task<Utilisateur> ObtenirUnUtilisateurAvecNumeroIdentifiant(string numeroIdentifiant)
        {
            var reponse = await _httpClient.GetAsync(_utilisateurApiUrl + "GetUtilisateurAvecNumeroIdentifiant/" + numeroIdentifiant);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<Utilisateur>();
        }

        public async Task AjouterUtilisateur(Utilisateur utilisateur)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(utilisateur), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PostAsync(_utilisateurApiUrl, content);

            JournalisationErreurAPI(reponse);
        }

        public async Task ModifierUtilisateur(Utilisateur utilisateur)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(utilisateur), Encoding.UTF8, "application/json");

            var result = await _httpClient.PutAsync(_utilisateurApiUrl + utilisateur.Id, content);

            JournalisationErreurAPI(result);

            await _httpClient.PutAsync(_utilisateurApiUrl + utilisateur.Id, content);
        }

        public async Task SupprimerUtilisateur(int id)
        {
            var reponse = await _httpClient.DeleteAsync(_utilisateurApiUrl + id);

            JournalisationErreurAPI(reponse);
        }

        public async Task<bool> UtilisateurExiste(int id)
        {
            var reponse = await _httpClient.GetAsync(_utilisateurApiUrl + "UtilisateurExists" + id);

            JournalisationErreurAPI(reponse);

            return await reponse.Content.ReadFromJsonAsync<bool>();
        }

        private void JournalisationErreurAPI(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode <= 499)
            {
                _logger.LogError(CustomLogEvenements.LogUtilisateurService, "(Bad Request) - Mauvaise requête du côté de l'API !");
            }

            if ((int)httpResponseMessage.StatusCode >= 500 && (int)httpResponseMessage.StatusCode <= 599)
            {
                _logger.LogCritical(CustomLogEvenements.LogUtilisateurService, "(Critical Error) - Erreur grave du côté de l'API !");
            }
        }
    }
}
