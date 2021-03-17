using byin_netcore_business.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetProductCategoriesByNameAsync(List<string> categoryNames);
        Task<List<ProductCategory>> GetAllProductCategoriesAsync();
    }
}
