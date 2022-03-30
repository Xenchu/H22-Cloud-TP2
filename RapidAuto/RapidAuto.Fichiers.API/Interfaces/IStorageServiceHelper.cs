namespace RapidAuto.Fichiers.API.Interfaces
{
    public interface IStorageServiceHelper
    {
        Task<IEnumerable<string>> ObtenirConteneurs();
        Task<string> ObtenirUrl(string nomConteneur);
        Task AjouterImages(List<IFormFile> images, string nomConteneur);
        Task<List<string>> ObtenirNomsImagesBlob(string nomConteneur, string codeUniqueVehicule);
    }
}
