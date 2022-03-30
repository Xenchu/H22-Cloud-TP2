using Microsoft.EntityFrameworkCore;
using RapidAuto.Commandes.API.Interfaces;
using RapidAuto.Commandes.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Commandes.API.Data
{
    public class AsyncRepository : IAsyncRepository
    {
        protected readonly CommandeContext _commandeContext;

        public AsyncRepository(CommandeContext commandeContext)
        {
            _commandeContext = commandeContext;
        }

        public async Task AddAsync(Commande commande)
        {
            await _commandeContext.Set<Commande>().AddAsync(commande);
            await _commandeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Commande commande)
        {
            _commandeContext.Set<Commande>().Remove(commande);
            await _commandeContext.SaveChangesAsync();
        }

        public async Task EditAsync(Commande commande)
        {
            _commandeContext.Entry<Commande>(_commandeContext.Commande.SingleOrDefault(c => c.Id == commande.Id)).State = EntityState.Detached;
            _commandeContext.Entry(commande).State = EntityState.Modified;
            await _commandeContext.SaveChangesAsync();
        }

        public async Task<Commande> GetByIdAsync(int id)
        {
            return await _commandeContext.Set<Commande>().FindAsync(id);
        }

        public async Task<IEnumerable<Commande>> ListAsync()
        {
            return await _commandeContext.Set<Commande>().ToListAsync();
        }

        public async Task<IEnumerable<Commande>> ListAsync(Expression<Func<Commande, bool>> predicate)
        {
            return await _commandeContext.Set<Commande>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
