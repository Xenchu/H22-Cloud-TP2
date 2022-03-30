using RapidAuto.Vehicules.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Vehicules.API.Interfaces
{
    public interface IAsyncRepository
    {
        Task<Vehicule> GetByIdAsync(int id);
        Task<IEnumerable<Vehicule>> ListAsync();
        Task<IEnumerable<Vehicule>> ListAsync(Expression<Func<Vehicule, bool>> predicate);
        Task AddAsync(Vehicule vehicule);
        Task DeleteAsync(Vehicule vehicule);
        Task EditAsync(Vehicule vehicule);
    }
}
