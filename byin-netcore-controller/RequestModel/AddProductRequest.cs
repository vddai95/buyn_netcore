using byin_netcore.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace byin_netcore.RequestModel
{
    [DataContract(Name = "AddProductRequest")]
    public class AddProductRequest
    {
        [Required(ErrorMessage = "product category is required")]
        [DataMember(Name = "ProductCategories")]
        public List<string> ProductCategories { get; set; }

        [Required(ErrorMessage = "price per unit is required")]
        [DataMember(Name = "PricePerUnit")]
        public double PricePerUnit { get; set; }

        [Required(ErrorMessage = "product name is required")]
        [DataMember(Name = "ProductName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "quantity available is required")]
        [DataMember(Name = "QuantityAvailable")]
        [Range(1, int.MaxValue)]
        public int QuantityAvailable { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "IllustrationImgs")]
        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtentions(new string[] { ".jpg", ".png", ".gif", ".jpeg"})]
        public List<IFormFile> IllustrationImgs { get; set; }
    }
}
