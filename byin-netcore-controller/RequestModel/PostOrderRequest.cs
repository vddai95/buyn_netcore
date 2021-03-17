using byin_netcore_transver;
using System;
using System.ComponentModel.DataAnnotations;
using byin_netcore.Attributes;

namespace byin_netcore.RequestModel
{
    public class PostOrderRequest
    {
        [Required]
        public int IdUser { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0.2, double.PositiveInfinity)]
        public decimal Price { get; set; }

        [In("vnd", "eur")]
        public string PriceUnit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Constants.DATE_TIME_FORMAT)]
        public DateTime DeliveryDate { get; set; }
    }
}
