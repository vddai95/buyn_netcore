using System.Collections.Generic;

namespace byin_netcore_business.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public List<ProductAndCategory> ProductCategoriesLink { get; set; }
        public double PricePerUnit { get; set; }
        public string ProductName { get; set; }
        public int QuantityAvailable { get; set; }
        public string Description { get; set; }
        public List<ProductAndImg> IllustrationImgLink { get; set; }
    }
}
