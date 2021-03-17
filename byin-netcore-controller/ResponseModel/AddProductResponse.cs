using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace byin_netcore.ResponseModel
{
    public class AddProductResponse
    {
        public int ProductId { get; set; }
        public List<string> ProductCategories { get; set; }
        public double PricePerUnit { get; set; }
        public string ProductName { get; set; }
        public int QuantityAvailable { get; set; }
        public string Description { get; set; }
        public List<string> IllustrationImgUrls { get; set; }
    }
}
