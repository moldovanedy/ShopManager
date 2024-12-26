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
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                products = await ctx.Categories.ToListAsync();
            }
            return products;
        }

        public static async Task<ValueResult<ProductCategory>> AddCategoryAsync(ProductCategory category)
        {
            ProductCategory insertedCategory;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                insertedCategory = ctx.Categories.Add(category);

                Error saveError = null;
                await Task.Run(async () =>
                {
                    try
                    {
                        await ctx.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        saveError = new Error(Error.ErrorType.Database, "An unknown database error occurred.");
                        Logger.LogError(ex.ToString());
                    }
                });

                if (saveError != null)
                {
                    return ValueResult<ProductCategory>.Failed(saveError);
                }
            }

            return ValueResult<ProductCategory>.Successful(insertedCategory);
        }

        public static async Task<Result> RemoveProductAsync(ProductCategory category)
        {
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                ctx.Categories.Remove(category);

                Error saveError = null;
                await Task.Run(async () =>
                {
                    try
                    {
                        await ctx.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        saveError = new Error(Error.ErrorType.Database, "An unknown database error occurred.");
                        Logger.LogError(ex.ToString());
                    }
                });

                if (saveError != null)
                {
                    return Result.Failed(saveError);
                }
            }

            return Result.Succesful();
        }

        public static async Task<ValueResult<ProductCategory>> UpdateProductAsync(ProductCategory category)
        {
            ProductCategory updatedCategory = null;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                Error saveError = null;
                await Task.Run(async () =>
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
                });

                if (saveError != null)
                {
                    return ValueResult<ProductCategory>.Failed(saveError);
                }
            }

            return ValueResult<ProductCategory>.Successful(updatedCategory);
        }
    }
}
