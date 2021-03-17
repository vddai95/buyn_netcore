using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_data.Model
{
    public class ProductAndCategory
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
