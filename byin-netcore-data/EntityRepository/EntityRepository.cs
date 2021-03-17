using byin_netcore_data.Interfaces;
using byin_netcore_data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace byin_netcore_data.EntityRepository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly MyContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if(_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            T entityToDelete = await _dbSet.FindAsync(id).ConfigureAwait(false);
            await DeleteAsync(entityToDelete);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> query)
        {
            if(query is null)
            {
                return await this.GetAllAsync().ConfigureAwait(false);
            }
            return await _dbSet.Where(query).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> InsertAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}