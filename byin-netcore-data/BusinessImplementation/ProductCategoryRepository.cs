using AutoMapper;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;

namespace byin_netcore_data.BusinessImplementation
{
    public class ProductCategoryRepository : Repository<BL.ProductCategory, DL.ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IMapper mapper, IEntityRepository<DL.ProductCategory> entityRepository) : base(mapper, entityRepository)
        {

        }

        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
        {
            var categories = await _entityRepository.GetAllAsync().ConfigureAwait(false);
            return categories.Select(c => _mapper.Map<ProductCategory>(c)).ToList();
        }

        public async Task<List<ProductCategory>> GetProductCategoriesByNameAsync(List<string> categoryNames)
        {
            var result = await this._entityRepository.GetWhereAsync(pc => categoryNames.Contains(pc.ProductCategoryName)).ConfigureAwait(false);
            if(result is null || !result.Any())
            {
                return new List<ProductCategory>();
            }
            return result.Select(r => _mapper.Map<ProductCategory>(r)).ToList();
        }
    }
}
