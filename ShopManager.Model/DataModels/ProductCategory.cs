using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Required]
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The category name can't have more than 255 characters")]
        public string Name { get; set; }
    }
}
