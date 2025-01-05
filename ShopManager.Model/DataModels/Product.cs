using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("Products")]
    public class Product
    {
        [Required]
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The name can't have more than 255 characters")]
        public string Name { get; set; } = "";

        [MaxLength(65535, ErrorMessage = "The description can't have more than 65 535 characters")]
        public string Description { get; set; } = "";

        [Required]
        public double Price { get; set; }

        [Required]
        public double PricePerKg { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.MinValue;

        [Required]
        public DateTime ExpiryDate { get; set; } = DateTime.MinValue;

        [Required]
        public double Quantity { get; set; }

        [Required]
        public long CategoryID { get; set; }
    }
}
