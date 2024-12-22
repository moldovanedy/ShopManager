using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Model.DataModels
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
