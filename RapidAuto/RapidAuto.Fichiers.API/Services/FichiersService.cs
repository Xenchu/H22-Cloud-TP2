using RapidAuto.Fichiers.API.Interfaces;

namespace RapidAuto.Fichiers.API.Services
{
    public class FichiersService : IFichiersService
    {
        // Path utilisé pour enregistrer\edit les images
        private string _path = @".\wwwroot\";

        public List<IFormFile> ConvertirBinaireAImage(List<string> imagesEnBinaire)
        {
            var images = new List<IFormFile>();
            var count = 1;
            var codeUnique = imagesEnBinaire.Last();

            imagesEnBinaire.RemoveAt(imagesEnBinaire.Count - 1);

            foreach (string imageEnBinaire in imagesEnBinaire)
            {
                byte[] bytes = Convert.FromBase64String(imageEnBinaire);
                MemoryStream stream = new MemoryStream(bytes);

                IFormFile image = new FormFile(stream, 0, bytes.Length, $"image{count}", $"{codeUnique}-image{count}.png");
                images.Add(image);
                count++;
            }

            return images;
        }

        public async Task EnregistrerImages(List<IFormFile> images)
        {
            foreach (var image in images)
            {
                var pathCombine = Path.Combine(_path, image.FileName);

                using (var stream = new FileStream(pathCombine, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }
        }

        public void GenererNomsImages(List<IFormFile> images, string codeUniqueDuVehicule)
        {
            int count = 1;

            foreach (var image in images)
            {
                var imageRenommee = new FileInfo(_path + image.FileName);

                if (imageRenommee.Exists)
                {
                    imageRenommee.MoveTo(String.Format(_path + codeUniqueDuVehicule + $"I{count}" + imageRenommee.Extension));
                    count++;
                }
            }
        }

        public void ModifierImage(List<string> nomImageEtNouveauCodeUnique)
        {
            var imageAModifier = new FileInfo(_path + nomImageEtNouveauCodeUnique[0]);

            if (imageAModifier.Exists)
            {
                if(nomImageEtNouveauCodeUnique[0].EndsWith("I1.png") == true)
                {
                    imageAModifier.MoveTo(String.Format(_path + nomImageEtNouveauCodeUnique[1] + $"I1.png"));
                }

                if (nomImageEtNouveauCodeUnique[0].EndsWith("I2.png") == true)
                {
                    imageAModifier.MoveTo(String.Format(_path + nomImageEtNouveauCodeUnique[1] + $"I2.png"));
                }
            }
        }

        public List<string> ObtenirNomsImages(string codeUniqueVehicule)
        {
            List<string> nomsDesImages = new List<string>();

            var dossier = new DirectoryInfo(_path);
            var listesDesImages = dossier.EnumerateFiles();

            foreach (var image in listesDesImages)
            {
                if (image.Name.Contains(codeUniqueVehicule))
                {
                    nomsDesImages.Add(image.Name);
                }
            }

            return nomsDesImages;
        }

        public bool SupprimerImage(string nomImage)
        {
            bool succes = false;

            if (File.Exists(_path + nomImage))
            {
                File.Delete(_path + nomImage);
                succes = true;
            }
            return succes;
        }
    }
}
