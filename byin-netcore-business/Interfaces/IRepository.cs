using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(object id);
        Task<T> UpdateAsync(T entity);
        Task<T> InsertAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
