using System.Data.Entity;
using ShopManager.Model.DataModels;

namespace ShopManager.Model.DBManager
{
    internal class AppDBContext : DbContext
    {
        internal static AppDBContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppDBContext();
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        private static AppDBContext _instance = null;

        private AppDBContext() : base("name=AppDBContext")
        { }

        internal DbSet<Product> Products { get; set; }
        internal DbSet<ProductCategory> Categories { get; set; }
        internal DbSet<Sale> Sales { get; set; }
    }
}
