using Microsoft.EntityFrameworkCore;
using RapidAuto.Utilisateurs.API.Interfaces;
using RapidAuto.Utilisateurs.API.Model;
using System.Linq.Expressions;

namespace RapidAuto.Utilisateurs.API.Data
{
    public class AsyncRepository : IAsyncRepository
    {
        protected readonly UtilisateursContext _utilisateursContext;

        public AsyncRepository(UtilisateursContext utilisateursContext)
        {
            _utilisateursContext = utilisateursContext;
        }

        public async Task AddAsync(Utilisateur utilisateur)
        {
            await _utilisateursContext.Set<Utilisateur>().AddAsync(utilisateur);
            await _utilisateursContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Utilisateur utilisateur)
        {
            _utilisateursContext.Set<Utilisateur>().Remove(utilisateur);
            await _utilisateursContext.SaveChangesAsync();
        }

        public async Task EditAsync(Utilisateur utilisateur)
        {
            _utilisateursContext.Entry<Utilisateur>(_utilisateursContext.Utilisateurs.FirstOrDefault(u => u.Id == utilisateur.Id)).State = EntityState.Detached;
            _utilisateursContext.Entry(utilisateur).State = EntityState.Modified;
            await _utilisateursContext.SaveChangesAsync();
        }

        public async Task<Utilisateur> GetByIdAsync(int id)
        {
            return await _utilisateursContext.Set<Utilisateur>().FindAsync(id);
        }

        public async Task<IEnumerable<Utilisateur>> ListAsync()
        {
            return await _utilisateursContext.Set<Utilisateur>().ToListAsync();
        }

        public async Task<IEnumerable<Utilisateur>> ListAsync(Expression<Func<Utilisateur, bool>> predicate)
        {
            return await _utilisateursContext.Set<Utilisateur>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
