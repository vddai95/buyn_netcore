using System.Collections.Generic;
using System.IO;

namespace byin_netcore_business.Inputs
{
    public class AddProductInput
    {
        public List<string> ProductCategories { get; set; }

        public double PricePerUnit { get; set; }

        public string ProductName { get; set; }

        public int QuantityAvailable { get; set; }

        public string Description { get; set; }

        public List<Stream> IllustrationImgs { get; set; }
    }
}
