using RapidAuto.Commandes.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Commandes.API.Interfaces
{
    public interface IAsyncRepository
    {
        Task<Commande> GetByIdAsync(int id);
        Task<IEnumerable<Commande>> ListAsync();
        Task<IEnumerable<Commande>> ListAsync(Expression<Func<Commande, bool>> predicate);
        Task AddAsync(Commande commande);
        Task DeleteAsync(Commande commande);
        Task EditAsync(Commande commande);
    }
}
