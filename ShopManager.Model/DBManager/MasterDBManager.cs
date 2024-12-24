using System.IO;
using System.Threading.Tasks;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class MasterDBManager
    {
        public static async Task InitializeDBAsync()
        {
            await Task.Run(
                async () =>
                {
                    bool hasDB = File.Exists("db.sqlite");

                    using (AppDBContext ctx = AppDBContext.Instance)
                    {
                        if (!hasDB)
                        {
                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"Products\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE," +
                                "\"Name\" TEXT NOT NULL," +
                                "\"Price\" REAL NOT NULL," +
                                "\"PricePerKg\" REAL," +
                                "\"Description\" TEXT NOT NULL," +
                                "\"PurchaseDate\" INTEGER NOT NULL," +
                                "\"ExpiryDate\" INTEGER NOT NULL," +
                                "\"Quantity\" INTEGER NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT) " +
                                "CONSTRAINT \"CategoryID\" FOREIGN KEY(\"CategoryID\") REFERENCES \"ProductCategories\"(\"ID\"))");

                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"ProductCategories\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE," +
                                "\"Name\" TEXT NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT))");

                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"Sales\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE, " +
                                "\"ProductID\" INTEGER, " +
                                "\"Quantity\" INTEGER NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT), " +
                                "CONSTRAINT \"ProductID\" FOREIGN KEY(\"ProductID\") REFERENCES \"Products\"(\"ID\"))");

                            await ctx.SaveChangesAsync();
                        }

                        //await Task.Run(() =>
                        //{
                        //    ctx.Products.Add(new Product()
                        //    {
                        //        Name = "Fasole",
                        //        Description = "smdm amf somd m omvo a",
                        //        ExpiryDate = new DateTime(2025, 11, 10),
                        //        PurchaseDate = DateTime.Now,
                        //        Quantity = 34
                        //    });
                        //});

                        await ctx.SaveChangesAsync();
                    }
                });
        }
    }
}
