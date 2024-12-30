using System.IO;
using System.Threading.Tasks;

namespace ShopManager.Model.DBManager
{
    public static class MasterDBManager
    {
        public static async Task InitializeDBAsync()
        {
            await Task.Run(
                async () =>
                {
                    bool hasDB = File.Exists("db.sqlite");

                    using (AppDBContext ctx = new AppDBContext())
                    {
                        if (!hasDB)
                        {
                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"Products\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE," +
                                "\"Name\" TEXT NOT NULL," +
                                "\"Description\" TEXT," +
                                "\"Price\" REAL NOT NULL DEFAULT 0," +
                                "\"PricePerKg\" REAL NOT NULL DEFAULT 0," +
                                "\"PurchaseDate\" INTEGER NOT NULL," +
                                "\"ExpiryDate\" INTEGER NOT NULL," +
                                "\"Quantity\" REAL NOT NULL DEFAULT 0, " +
                                "\"CategoryID\" INTEGER NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT) " +
                                "CONSTRAINT \"CategoryID\" FOREIGN KEY(\"CategoryID\") REFERENCES \"ProductCategories\"(\"ID\"))");

                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"ProductCategories\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE," +
                                "\"Name\" TEXT NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT))");

                            ctx.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS \"Sales\" (" +
                                "\"ID\" INTEGER NOT NULL UNIQUE, " +
                                "\"ProductID\" INTEGER NOT NULL, " +
                                "\"Quantity\" REAL NOT NULL, " +
                                "PRIMARY KEY(\"ID\" AUTOINCREMENT), " +
                                "CONSTRAINT \"ProductID\" FOREIGN KEY(\"ProductID\") REFERENCES \"Products\"(\"ID\"))");

                            await ctx.SaveChangesAsync();
                        }

                        //ctx.Products.Add(new Product()
                        //{
                        //    Name = "Fasole",
                        //    Description = "smdm amf somd m omvo a",
                        //    ExpiryDate = new DateTime(2025, 11, 10),
                        //    PurchaseDate = DateTime.Now,
                        //    Quantity = 34
                        //});

                        await ctx.SaveChangesAsync();
                    }
                });
        }
    }
}
