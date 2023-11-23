using Miro.Server.Entities;
using Miro.Server.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Miro.Server.Services
{
    public class UserRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DBContext _dBContext;

        public UserRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task AddAsync (TEntity entity)
        {
            await _dBContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dBContext.Set<TEntity>()
                .FirstOrDefaultAsync(filter)
                .ConfigureAwait(false);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id).ConfigureAwait(false);
            if (entity == null)
                return;

            _dBContext.Set<TEntity>().Remove(entity);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task GetAll()
        {

        }

        public async Task<bool> ExistsAsync(int id)
        {
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dBContext.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dBContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync()
        {
            return await _dBContext.Set<TEntity>().CountAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dBContext.Set<TEntity>().Update(entity);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
