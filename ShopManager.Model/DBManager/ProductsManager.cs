using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class ProductsManager
    {
        public static async Task<List<Product>> GetAllProductsAsync(int skip = 0, int limit = 100)
        {
            List<Product> products = new List<Product>();
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    skip = Math.Max(0, Math.Min(100, skip));
                    limit = Math.Max(0, Math.Min(100, limit));

                    int noOfProducts = await ctx.Products.CountAsync();
                    if (noOfProducts < (skip * 100))
                    {
                        skip = noOfProducts - (noOfProducts % 100);
                    }

                    products =
                            await ctx
                                .Products.AsNoTracking()
                                .OrderBy(product => product.ID)
                                //.Skip(() => skip)
                                //.Take(() => limit)
                                .ToListAsync();
                }
            });
            return products;
        }

        public static async Task<uint> GetNumberOfProductsAsync()
        {
            uint total = 0;
            using (AppDBContext ctx = new AppDBContext())
            {
                total = (uint)await ctx.Products.CountAsync();
            }

            return total;
        }

        public static async Task<Result> AddProductAsync(Product product)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        product = ctx.Products.Add(product);
                        await ctx.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        saveError = new Error(Error.ErrorType.Database, "An unknown database error occurred.");
                        Logger.LogError(ex.ToString());
                    }
                }
            });

            if (saveError != null)
            {
                return Result.Failed(saveError);
            }

            return Result.Successful();
        }

        public static async Task<Result> DeleteProductAsync(Product product)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        ctx.Products.Attach(product);

                        ctx.Products.Remove(product);
                        await ctx.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        saveError = new Error(Error.ErrorType.Database, "An unknown database error occurred.");
                        Logger.LogError(ex.ToString());
                    }
                }
            });

            if (saveError != null)
            {
                return Result.Failed(saveError);
            }

            return Result.Successful();
        }

        /// <summary>
        /// Updates the given product if one with the corresponding ID is found, otherwise calls 
        /// <see cref="AddProductAsync(Product)"/> and returns its response.
        /// </summary>
        /// <remarks>
        /// If you know a product doesn't exist and want to add it, use <see cref="AddProductAsync(Product)"/> 
        /// as it is a little bit faster. This should be used for adding only when you don't know if a product exists or not.
        /// </remarks>
        /// <param name="product">The product to update (or add if one doesn't exist). Must have its ID set correctly for updating.</param>
        /// <returns></returns>
        public static async Task<Result> AddOrUpdateProductAsync(Product product)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        Product storedProduct = await ctx.Products.FindAsync(product.ID);
                        if (storedProduct == null)
                        {
                            Result addResult = await AddProductAsync(product);
                            saveError = addResult.ResultingError;
                            return;
                        }

                        ctx.Entry(storedProduct).CurrentValues.SetValues(product);
                        await ctx.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        saveError = new Error(Error.ErrorType.Database, "An unknown database error occurred.");
                        Logger.LogError(ex.ToString());
                    }
                }
            });

            if (saveError != null)
            {
                return Result.Failed(saveError);
            }

            return Result.Successful();
        }
    }
}
