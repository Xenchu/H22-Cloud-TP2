using Microsoft.EntityFrameworkCore;
using RapidAuto.Vehicules.API.Interfaces;
using RapidAuto.Vehicules.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Vehicules.API.Data
{
    public class AsyncRepository : IAsyncRepository
    {
        protected readonly VehiculeContext _vehiculeContext;

        public AsyncRepository(VehiculeContext vehiculeContext)
        {
            _vehiculeContext = vehiculeContext;
        }

        public async Task AddAsync(Vehicule vehicule)
        {
            await _vehiculeContext.Set<Vehicule>().AddAsync(vehicule);
            await _vehiculeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vehicule vehicule)
        {
            _vehiculeContext.Set<Vehicule>().Remove(vehicule);
            await _vehiculeContext.SaveChangesAsync();
        }

        public async Task EditAsync(Vehicule vehicule)
        {
            _vehiculeContext.Entry<Vehicule>(_vehiculeContext.Vehicule.SingleOrDefault(v => v.Id == vehicule.Id)).State = EntityState.Detached;
            _vehiculeContext.Entry(vehicule).State = EntityState.Modified;
            await _vehiculeContext.SaveChangesAsync();
        }

        public async Task<Vehicule> GetByIdAsync(int id)
        {
            return await _vehiculeContext.Set<Vehicule>().FindAsync(id);
        }

        public async Task<IEnumerable<Vehicule>> ListAsync()
        {
            return await _vehiculeContext.Set<Vehicule>().ToListAsync();
        }

        public async Task<IEnumerable<Vehicule>> ListAsync(Expression<Func<Vehicule, bool>> predicate)
        {
            return await _vehiculeContext.Set<Vehicule>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
