using RapidAuto.Utilisateurs.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Utilisateurs.API.Interfaces
{
    public interface IAsyncRepository
    {
        Task<Utilisateur> GetByIdAsync(int id);
        Task<IEnumerable<Utilisateur>> ListAsync();
        Task<IEnumerable<Utilisateur>> ListAsync(Expression<Func<Utilisateur, bool>> predicate);
        Task AddAsync(Utilisateur vehicule);
        Task DeleteAsync(Utilisateur vehicule);
        Task EditAsync(Utilisateur vehicule);
    }
}
