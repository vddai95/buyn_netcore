using byin_netcore_business.Entity;
using byin_netcore_business.Entity.File;
using byin_netcore_business.Inputs;
using byin_netcore_business.Interfaces;
using byin_netcore_business.UseCases.Base;
using byin_netcore_transver.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.ProductBusiness
{
    public class ProductBusiness : BaseBusiness, IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileBusiness _fileBusiness;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductBusiness(IProductRepository productRepository, 
            IAuthorizationBusiness authorizationService,
            IFileBusiness fileBusiness,
            IProductCategoryRepository productCategoryRepository
            ) : base(authorizationService)
        {
            _productRepository = productRepository;
            _fileBusiness = fileBusiness;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<Product> AddProductAsync(AddProductInput addProductInput)
        {
            var filePaths = new List<FilePath>();
            var productCategories = new List<ProductCategory>();
            try
            {
                var uploadTasks = Task.WhenAll(addProductInput.IllustrationImgs.Select(
                img => _fileBusiness.AddFileAsync(img).ContinueWith(
                    r => filePaths.Add(r.Result))));
                var getProductCategories = _productCategoryRepository.GetProductCategoriesByNameAsync(addProductInput.ProductCategories);
                await uploadTasks.ConfigureAwait(false);
                productCategories = await getProductCategories.ConfigureAwait(false);
            }
            catch (AggregateException ex)
            {
                await Task.WhenAll(filePaths.Select(fp => _fileBusiness.DeleteFileAsync(fp.CloudStorageKey)));
                throw ex;
            }

            var product = new Product
            {
                Description = addProductInput.Description,
                PricePerUnit = addProductInput.PricePerUnit,
                ProductName = addProductInput.ProductName,
                QuantityAvailable = addProductInput.QuantityAvailable,
                IllustrationImgLink = filePaths.Select(fp => new ProductAndImg { 
                    FilePathId = fp.Id
                }).ToList(),
                ProductCategoriesLink = productCategories.Select(pc => new ProductAndCategory
                {
                    ProductCategoryId = pc.Id
                }).ToList()
            };

            var isAuthorized = await _authorizationService.AuthorizeAsync(product, BaseOperation.Create).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                await Task.WhenAll(filePaths.Select(fp => _fileBusiness.DeleteFileAsync(fp.CloudStorageKey)));
                throw new HttpUnAuthorizedException();
            }

            try
            {
                return await _productRepository.InsertAsync(product).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await Task.WhenAll(filePaths.Select(fp => _fileBusiness.DeleteFileAsync(fp.CloudStorageKey)));
                throw ex;
            }
        }

        public async Task<ProductCategory> AddProductCategoryAsync(string productCategory)
        {
            var newCategory = new ProductCategory
            {
                ProductCategoryName = productCategory
            };

            var isAuthorized = await _authorizationService.AuthorizeAsync(newCategory, BaseOperation.Create).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }

            return await _productCategoryRepository.InsertAsync(newCategory).ConfigureAwait(false);
        }

        public async Task DeleteProductAsync(Product product)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(product, BaseOperation.Delete).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }

            await _productRepository.DeleteAsync(product).ConfigureAwait(false);
        }

        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(new ProductCategory(), BaseOperation.ReadAll).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }
            return await _productCategoryRepository.GetAllProductCategoriesAsync().ConfigureAwait(false);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync().ConfigureAwait(false);
            foreach (var product in products)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(new Product(), BaseOperation.ReadAll).ConfigureAwait(false);
                if (!isAuthorized.Succeeded)
                {
                    throw new HttpUnAuthorizedException();
                }
            }

            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await _productRepository.GetProductByCatergoryAsync(categoryName).ConfigureAwait(false);
            foreach(var product in products)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(product, BaseOperation.Read).ConfigureAwait(false);
                if (!isAuthorized.Succeeded)
                {
                    throw new HttpUnAuthorizedException();
                }
            }

            return products;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(product, BaseOperation.Update).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }

            return await _productRepository.UpdateAsync(product).ConfigureAwait(false);
        }
    }
}
