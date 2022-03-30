using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RapidAuto.Favoris.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidAuto.Favoris.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavorisController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        private static List<int> _cacheKeys = new List<int>();

        public FavorisController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET: api/<FavorisController>
        [HttpGet]
        public List<Vehicule> GetFavoris()
        {
            List<int> cacheKeysExpired = new List<int>();

            List<Vehicule> listeVehiculesFavoris = new List<Vehicule>();

            foreach (var key in _cacheKeys)
            {
                if (_memoryCache.TryGetValue(key, out Vehicule favori))
                {
                    listeVehiculesFavoris.Add(favori);
                }
                else
                {
                    cacheKeysExpired.Add(key);
                }
            }

            foreach (var key in cacheKeysExpired)
            {
                _cacheKeys.Remove(key);
            }

            return listeVehiculesFavoris;
        }

        // POST api/<FavorisController>
        [HttpPost]
        public async Task<IActionResult> PostFavori([FromBody] Vehicule vehicule)
        {
            if (!_memoryCache.TryGetValue(vehicule.Id, out Vehicule favori))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                };

                _memoryCache.Set(vehicule.Id, vehicule, cacheEntryOptions);

                _cacheKeys.Add(vehicule.Id);

                return NoContent();
            }
            else
            {
                return Content("Le véhicule est déjà dans vos favoris.");
            }
        }

        // DELETE api/<FavorisController>/5
        [HttpDelete("{idVehicule}")]
        public async Task<IActionResult> DeleteFavori(int idVehicule)
        {
            if (_memoryCache.TryGetValue(idVehicule, out Vehicule favori))
            {
                _memoryCache.Remove(idVehicule);

                _cacheKeys.Remove(idVehicule);

                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
