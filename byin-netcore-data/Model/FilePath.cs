using System.Collections.Generic;

namespace byin_netcore_data.Model
{
    public class FilePath
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string CloudStorageKey { get; set; }

        public virtual ICollection<ProductAndImg> Products { get; set; }
    }
}
