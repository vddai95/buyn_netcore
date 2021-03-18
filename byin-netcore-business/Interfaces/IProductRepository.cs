using byin_netcore_business.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductByCatergoryAsync(string catergoryName);
        Task<List<Product>> GetAllProductsAsync();
    }
}
