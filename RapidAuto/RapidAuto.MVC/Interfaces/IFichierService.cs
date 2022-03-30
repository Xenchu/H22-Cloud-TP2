namespace RapidAuto.MVC.Interfaces
{
    public interface IFichierService
    {
        Task<string> ObtenirUrlConteneur(string nomConteneur);
        Task<List<string>> ObtenirNomsImages(string codeUniqueVehicule);
        Task AjouterImages(List<string> imagesConverties, string codeUniqueVehicule);
        Task SupprimerImage(string nomImage);
        Task<string> ModifierImage(List<string> nomImageEtNouveauCodeUnique);
        Task<List<string>> ConvertirImagesEnBytes(List<IFormFile> images);
    }
}
