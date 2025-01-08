using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class CategoriesManager
    {
        public static async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            List<ProductCategory> products = new List<ProductCategory>();
            using (AppDBContext ctx = new AppDBContext())
            {
                products = await ctx.Categories.AsNoTracking().ToListAsync();
                if (products.Count == 0)
                {
                    ProductCategory defaultCategory = new ProductCategory()
                    {
                        Name = ""
                    };
                    ctx.Categories.Add(defaultCategory);
                    await ctx.SaveChangesAsync();

                    products.Add(defaultCategory);
                }
            }
            return products;
        }

        public static async Task<ValueResult<ProductCategory>> AddCategoryAsync(ProductCategory category)
        {
            ProductCategory insertedCategory = null;
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        insertedCategory = ctx.Categories.Add(category);
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
                return ValueResult<ProductCategory>.Failed(saveError);
            }

            return ValueResult<ProductCategory>.Successful(insertedCategory);
        }

        public static async Task<Result> DeleteCategoryAsync(ProductCategory category)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        ctx.Categories.Attach(category);

                        ctx.Categories.Remove(category);
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

        public static async Task<Result> AddOrUpdateCategoryAsync(ProductCategory category)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        long oldID = category.ID;
                        ProductCategory storedCategory = await ctx.Categories.FindAsync(category.ID);
                        if (storedCategory == null)
                        {
                            Result addResult = await AddCategoryAsync(category);
                            saveError = addResult.ResultingError;
                        }
                        else
                        {
                            ctx.Entry(storedCategory).CurrentValues.SetValues(category);
                            await ctx.SaveChangesAsync();
                        }

                        if (category.ID == oldID)
                        {
                            return;
                        }

                        //update the categories IDs
                        ProductCache.GetAllProductsFromCurrentPage()
                            .Where((prod) => prod.CategoryID == oldID)
                            .ToList()
                            .ForEach((prod) =>
                                {
                                    prod.CategoryID = category.ID;
                                });
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
