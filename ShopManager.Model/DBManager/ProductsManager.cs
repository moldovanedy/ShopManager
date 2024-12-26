using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class ProductsManager
    {
        public static async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = new List<Product>();
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                products = await ctx.Products.ToListAsync();
            }
            return products;
        }

        public static async Task<ValueResult<Product>> AddProductAsync(Product product)
        {
            Product insertedProduct;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                insertedProduct = ctx.Products.Add(product);

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
                    return ValueResult<Product>.Failed(saveError);
                }
            }

            return ValueResult<Product>.Successful(insertedProduct);
        }

        public static async Task<Result> RemoveProductAsync(Product product)
        {
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                ctx.Products.Remove(product);

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

        public static async Task<ValueResult<Product>> UpdateProductAsync(Product product)
        {
            Product updatedProduct = null;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                Error saveError = null;
                await Task.Run(async () =>
                {
                    try
                    {
                        Product storedProduct = ctx.Products.Find(product.ID);
                        if (storedProduct == null)
                        {
                            updatedProduct = product;
                            return;
                        }

                        //storedProduct.Name = product.Name;
                        //storedProduct.Description = product.Description;
                        //storedProduct.PurchaseDate = product.PurchaseDate;
                        //storedProduct.ExpiryDate = product.ExpiryDate;
                        //storedProduct.Quantity = product.Quantity;
                        //storedProduct.CategoryID = product.CategoryID;
                        ctx.Entry(storedProduct).CurrentValues.SetValues(product);

                        updatedProduct = storedProduct;
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
                    return ValueResult<Product>.Failed(saveError);
                }
            }

            return ValueResult<Product>.Successful(updatedProduct);
        }
    }
}
