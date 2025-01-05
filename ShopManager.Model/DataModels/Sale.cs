using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("Sales")]
    public class Sale
    {
        [Required]
        [Key]
        public long ID { get; set; }

        [Required]
        public long ProductID { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
