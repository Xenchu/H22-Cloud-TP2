using Newtonsoft.Json;
using RapidAuto.MVC.Interfaces;
using System.Net;
using System.Text;

namespace RapidAuto.MVC.Services
{
    public class FichierServiceProxy : IFichierService
    {
        private readonly HttpClient _httpClient;
        private const string _fichierApiUrl = "api/Fichier/";
        private readonly ILogger<FichierServiceProxy> _logger;

        public FichierServiceProxy(HttpClient httpClient, ILogger<FichierServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> ObtenirUrlConteneur(string nomConteneur)
        {
            var reponse = await _httpClient.GetAsync(_fichierApiUrl + "GetUrlConteneur/" + nomConteneur);
            JournalisationErreurAPI(reponse);
            return await reponse.Content.ReadAsStringAsync();
        }

        public async Task<List<string>> ObtenirNomsImages(string codeUniqueVehicule)
        {
            var reponse = await _httpClient.GetAsync(_fichierApiUrl + codeUniqueVehicule);
            JournalisationErreurAPI(reponse);
            return await reponse.Content.ReadFromJsonAsync<List<string>>();
        }

        public async Task AjouterImages(List<string> imagesConverties, string codeUniqueVehicule)
        {
            imagesConverties.Add(codeUniqueVehicule);
            StringContent content = new StringContent(JsonConvert.SerializeObject(imagesConverties), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PostAsync(_fichierApiUrl, content);

            JournalisationErreurAPI(reponse);
        }

        public async Task<string> ModifierImage(List<string> nomImageEtNouveauCodeUnique)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(nomImageEtNouveauCodeUnique), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PutAsync(_fichierApiUrl, content);

            JournalisationErreurAPI(reponse);

            var autreReponse = await _httpClient.GetAsync(_fichierApiUrl + nomImageEtNouveauCodeUnique[1]);

            JournalisationErreurAPI(autreReponse);

            var listeTempAvecNouveauNomImage = await autreReponse.Content.ReadFromJsonAsync<List<string>>();

            if (listeTempAvecNouveauNomImage.Count == 1)
            {
                return listeTempAvecNouveauNomImage[0];
            }
            else
            {
                return listeTempAvecNouveauNomImage[1];
            } 
        }

        public async Task SupprimerImage(string nomImage)
        {
            var reponse = await _httpClient.DeleteAsync(_fichierApiUrl + nomImage);

            JournalisationErreurAPI(reponse);
        }

        public async Task<List<string>> ConvertirImagesEnBytes(List<IFormFile> images)
        {
            List<string> imagesConverties = new List<string>();

            foreach (var image in images)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    imagesConverties.Add(Convert.ToBase64String(stream.ToArray()));
                }
            }
            return imagesConverties;
        }

        private void JournalisationErreurAPI(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode <= 499)
            {
                _logger.LogError(CustomLogEvenements.LogFichierService, "(Bad Request) - Mauvaise requête du côté de l'API !");
            }

            if ((int)httpResponseMessage.StatusCode >= 500 && (int)httpResponseMessage.StatusCode <= 599)
            {
                _logger.LogCritical(CustomLogEvenements.LogFichierService, "(Critical Error) - Erreur grave du côté de l'API !");
            }
        }
    }
}
