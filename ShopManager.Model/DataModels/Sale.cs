using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("Sales")]
    public class Sale
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public uint Quantity { get; set; }
    }
}
