using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RapidAuto.Fichiers.API.Interfaces;

namespace RapidAuto.Fichiers.API.Services
{
    public class StorageServiceHelper : IStorageServiceHelper
    {
        private readonly BlobServiceClient _blobServiceClient;

        public StorageServiceHelper(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task AjouterImages(List<IFormFile> images, string nomConteneur)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(nomConteneur);

            foreach (var image in images)
                using (var stream = image.OpenReadStream())
                    await containerClient.UploadBlobAsync(image.FileName, stream);
        }

        public async Task<IEnumerable<string>> ObtenirConteneurs()
        {
            var conteneurs = new List<string>();

            await foreach (var containerItem in _blobServiceClient.GetBlobContainersAsync(BlobContainerTraits.Metadata))
                conteneurs.Add(containerItem.Name);

            return conteneurs;
        }

        public async Task<List<string>> ObtenirNomsImagesBlob(string nomConteneur, string codeUniqueVehicule)
        {
            var nomImages = new List<string>();

            var containerClient = _blobServiceClient.GetBlobContainerClient(nomConteneur);

            await foreach (var blob in containerClient.GetBlobsAsync())
                if (blob.Name.Contains(codeUniqueVehicule))
                    nomImages.Add(blob.Name);

            return nomImages;
        }

        public async Task<string> ObtenirUrl(string nomConteneur)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(nomConteneur);

            var url = $"{containerClient.Uri.AbsoluteUri}/";

            return url;
        }
    }
}
