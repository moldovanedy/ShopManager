using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ShopManager.Controller.DataModels;
using ShopManager.Model.DataModels;

namespace ShopManager.Model.DBManager
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() :
            base("name=AppDBContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
