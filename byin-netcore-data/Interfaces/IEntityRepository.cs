using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace byin_netcore_data.Interfaces
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<T> UpdateAsync(T entity);
        Task<T> InsertAsync(T entity);
        Task DeleteAsync(object id);
        Task DeleteAsync(T entity);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> query);
        IQueryable<T> GetAll();
    }
}
