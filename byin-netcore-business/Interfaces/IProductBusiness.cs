using byin_netcore_business.Entity;
using byin_netcore_business.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IProductBusiness
    {
        Task<Product> AddProductAsync(AddProductInput product);
        Task DeleteProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<ProductCategory> AddProductCategoryAsync(string productCategory);
        Task<List<ProductCategory>> GetAllProductCategoriesAsync();
    }
}
