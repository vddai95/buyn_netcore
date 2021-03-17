using System.Collections.Generic;

namespace byin_netcore_data.Model
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string ProductCategoryName { get; set; }
        public virtual ICollection<ProductAndCategory> ProductsLink { get; set; }
    }
}
