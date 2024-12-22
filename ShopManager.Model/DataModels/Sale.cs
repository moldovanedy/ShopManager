using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("Sales")]
    public class Sale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
