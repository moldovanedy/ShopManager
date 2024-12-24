using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("Products")]
    public class Product
    {
        public int ID { get; set; }

        [MaxLength(255, ErrorMessage = "The name can't have more than 255 characters")]
        public string Name { get; set; }

        [MaxLength(65535, ErrorMessage = "The description can't have more than 65 535 characters")]
        public string Description { get; set; }
        public double Price { get; set; }
        public double PricePerKg { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public uint Quantity { get; set; }

        [Required]
        [ForeignKey(nameof(ProductCategory))]
        public int CategoryID { get; set; }
    }
}
