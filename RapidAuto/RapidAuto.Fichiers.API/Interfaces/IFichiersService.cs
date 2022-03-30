namespace RapidAuto.Fichiers.API.Interfaces
{
    public interface IFichiersService
    {
        void GenererNomsImages(List<IFormFile> images, string codeUniqueDuVehicule);

        List<IFormFile> ConvertirBinaireAImage(List<string> imagesEnBinaire);
        Task EnregistrerImages(List<IFormFile> images);

        List<string> ObtenirNomsImages(string codeUniqueVehicule);

        bool SupprimerImage(string nomImage);

        void ModifierImage(List<string> nomImageEtNouveauCodeUnique);
    }
}
