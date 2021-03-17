using byin_netcore_business.Entity.File;
using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_business.Entity
{
    public class ProductAndImg
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int FilePathId { get; set; }
        public virtual FilePath FilePath { get; set; }
    }
}
