using Microsoft.AspNetCore.Mvc;
using RapidAuto.Fichiers.API.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidAuto.Fichiers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichierController : ControllerBase
    {
        private readonly IFichiersService _fichiersService;
        private readonly IStorageServiceHelper _storageServiceHelper;

        public FichierController(IFichiersService fichiersService, IStorageServiceHelper storageServiceHelper)
        {
            _fichiersService = fichiersService;
            _storageServiceHelper = storageServiceHelper;
        }

        // GET api/<FichierController>/images
        [HttpGet()]
        [Route("[action]/{nomConteneur}")]
        public async Task<ActionResult<string>> GetUrlConteneur(string nomConteneur)
        {
            if (nomConteneur == null)
                return NotFound();

            return await _storageServiceHelper.ObtenirUrl(nomConteneur);
        }

        // GET api/<FichierController>/5
        [HttpGet("{codeUniqueVehicule}")]
        public async Task<ActionResult<List<string>>> GetNomsImages(string codeUniqueVehicule)
        {
            var conteneurs = await _storageServiceHelper.ObtenirConteneurs();
            var nomConteneur = conteneurs.FirstOrDefault(c => c == "images");

            if (nomConteneur == null)
                return NotFound();

            var nomsImages = await _storageServiceHelper.ObtenirNomsImagesBlob(nomConteneur, codeUniqueVehicule);

            if (nomsImages.Count <= 0)
                return NotFound();

            return nomsImages;
        }

        // POST api/<FichierController>
        [HttpPost]
        public async Task Post([FromBody] List<string> imagesEtCodeUnique)
        {
            var images = _fichiersService.ConvertirBinaireAImage(imagesEtCodeUnique);
            var conteneurs = await _storageServiceHelper.ObtenirConteneurs();
            var nomConteneur = conteneurs.FirstOrDefault(c => c == "images");

            if (nomConteneur == null)
                return;

            await _storageServiceHelper.AjouterImages(images, nomConteneur);
        }

        // POST api/<FichierController>/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] List<string> nomImageEtNouveauCodeUnique)
        {
             _fichiersService.ModifierImage(nomImageEtNouveauCodeUnique);

            return NoContent();
        }


        // DELETE api/<FichierController>/5
        [HttpDelete("{nomImage}")]
        public async Task<ActionResult> Delete(string nomImage)
        {
            var succes = _fichiersService.SupprimerImage(nomImage);

            if (succes == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
