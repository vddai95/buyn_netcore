using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_business.Entity
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public double PricePerUnit { get; set; }
        public short Quantity { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
