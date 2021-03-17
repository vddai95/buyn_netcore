using byin_netcore_business.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_business.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public virtual User Customer { get; set; }

        public string CustomerIpAdress { get; set; }
        public List<OrderEntity> OrderEntities { get; set; }
        public OrderStatusName OrderStatus { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime SubmitedDate { get; set; }
        public string NoteFromCustomer { get; set; }
        public string City { get; set; }
        public string TelephoneNumer { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

    }
}
