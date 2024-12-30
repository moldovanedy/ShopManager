using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
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
                products = await ctx.Categories.ToListAsync();
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
                    insertedCategory = ctx.Categories.Add(category);

                    try
                    {
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

        public static async Task<Result> RemoveProductAsync(ProductCategory category)
        {

            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    ctx.Categories.Remove(category);
                    try
                    {
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

        public static async Task<ValueResult<ProductCategory>> UpdateProductAsync(ProductCategory category)
        {
            ProductCategory updatedCategory = null;
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        ProductCategory storedCategory = ctx.Categories.Find(category.ID);
                        if (storedCategory == null)
                        {
                            updatedCategory = category;
                            return;
                        }

                        ctx.Entry(storedCategory).CurrentValues.SetValues(category);

                        updatedCategory = storedCategory;
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

            return ValueResult<ProductCategory>.Successful(updatedCategory);
        }
    }
}
