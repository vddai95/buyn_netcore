using AutoMapper;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;

namespace byin_netcore_data.BusinessImplementation
{
    public class ProductRepository : Repository<BL.Product, DL.Product>, IProductRepository
    {
        public ProductRepository(IMapper mapper, IEntityRepository<DL.Product> entityRepository) : base(mapper, entityRepository)
        {

        }

        public async Task<List<Product>> GetProductByCatergoryAsync(string catergoryName)
        {
            var products = await _entityRepository.GetWhere(
                p => p.ProductCategoriesLink.Any(l => l.ProductCategory.ProductCategoryName == catergoryName))
                .Include(p => p.IllustrationImgLink).ThenInclude(l => l.FilePath)
                .Include(p => p.ProductCategoriesLink).ThenInclude(l => l.ProductCategory)
                .ToListAsync().ConfigureAwait(false);
            return products.Select(p => _mapper.Map<BL.Product>(p)).ToList();
        }
    }
}
