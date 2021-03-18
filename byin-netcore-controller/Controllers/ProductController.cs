using System.Linq;
using System.Threading.Tasks;
using byin_netcore.RequestModel;
using byin_netcore.ResponseModel;
using byin_netcore_business.Inputs;
using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace byin_netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet("GetAllProductCategories")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            return Ok(await _productBusiness.GetAllProductCategoriesAsync().ConfigureAwait(false));
        }

        [HttpPost("AddProductCategory")]
        public async Task<IActionResult> AddProductCategory(string ProductCategoryName)
        {
            return Ok(await _productBusiness.AddProductCategoryAsync(ProductCategoryName).ConfigureAwait(false));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddProductRequest model)
        {
            var addProductInput = new AddProductInput
            {
                Description = model.Description,
                PricePerUnit = model.PricePerUnit,
                ProductName = model.ProductName,
                QuantityAvailable = model.QuantityAvailable,
                ProductCategories = model.ProductCategories,
                IllustrationImgs = model.IllustrationImgs.Select(f => f.OpenReadStream()).ToList()
            };

            var product = await _productBusiness.AddProductAsync(addProductInput).ConfigureAwait(false);
            
            var response = new AddProductResponse
            {
                ProductId = product.Id,
                Description = product.Description,
                PricePerUnit = product.PricePerUnit,
                ProductName = product.ProductName,
                QuantityAvailable = product.QuantityAvailable,
                IllustrationImgUrls = product.IllustrationImgLink.Select(url => url.FilePath.ImagePath).ToList(),
                ProductCategories = product.ProductCategoriesLink.Select(c => c.ProductCategory.ProductCategoryName).ToList()
            };
            return Ok(response);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> Get(string categoryName)
        {
            var products = await _productBusiness.GetProductsByCategoryAsync(categoryName).ConfigureAwait(false);
            var response = products.Select(p => new GetProductResponse
            {
                ProductId = p.Id,
                Description = p.Description,
                PricePerUnit = p.PricePerUnit,
                ProductName = p.ProductName,
                QuantityAvailable = p.QuantityAvailable,
                IllustrationImgUrls = p.IllustrationImgLink.Select(url => url.FilePath.ImagePath).ToList(),
                ProductCategories = p.ProductCategoriesLink.Select(c => c.ProductCategory.ProductCategoryName).ToList()
            });
            return Ok(response);
        }
    }
}
