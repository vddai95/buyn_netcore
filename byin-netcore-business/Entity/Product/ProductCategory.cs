﻿using System.Collections.Generic;

namespace byin_netcore_business.Entity
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string ProductCategoryName { get; set; }
        public virtual ICollection<ProductAndCategory> ProductsLink { get; set; }
    }
}
