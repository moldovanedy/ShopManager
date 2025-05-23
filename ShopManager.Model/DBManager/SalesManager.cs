﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class SalesManager
    {
        public static async Task<List<Sale>> GetAllSalesAsync()
        {
            List<Sale> sales = new List<Sale>();

            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    sales =
                            await ctx.Sales.AsNoTracking()
                                .OrderBy(product => product.ID)
                                .ToListAsync();
                }
            });
            return sales;
        }

        public static async Task<uint> GetNumberOfSalesAsync()
        {
            uint total = 0;
            using (AppDBContext ctx = new AppDBContext())
            {
                total = (uint)await ctx.Sales.CountAsync();
            }

            return total;
        }

        public static async Task<ValueResult<Sale>> AddSaleAsync(Sale sale)
        {
            Sale insertedSale = null;

            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        insertedSale = ctx.Sales.Add(sale);
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
                return ValueResult<Sale>.Failed(saveError);
            }

            return ValueResult<Sale>.Successful(insertedSale);
        }

        public static async Task<Result> DeleteSaleAsync(Sale sale)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        ctx.Sales.Attach(sale);

                        ctx.Sales.Remove(sale);
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
        /// Updates the given sale if one with the corresponding ID is found, otherwise calls 
        /// <see cref="AddSaleAsync(Sale)"/> and returns its response.
        /// </summary>
        /// <remarks>
        /// If you know a sale doesn't exist and want to add it, use <see cref="AddSaleAsync(Sale)"/> 
        /// as it is a little bit faster. This should be used for adding only when you don't know if a sale exists or not.
        /// </remarks>
        /// <param name="sale">The sale to update (or add if one doesn't exist). Must have its ID set correctly for updating.</param>
        /// <returns></returns>
        public static async Task<Result> AddOrUpdateSaleAsync(Sale sale)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        Sale storedSale = await ctx.Sales.FindAsync(sale.ID);
                        if (storedSale == null)
                        {
                            Result addResult = await AddSaleAsync(sale);
                            saveError = addResult.ResultingError;
                            return;
                        }

                        ctx.Entry(storedSale).CurrentValues.SetValues(sale);
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
        /// Works the same as <see cref="AddOrUpdateSaleAsync(Sale)"/>, but doing it for multiple objects at once.
        /// </summary>
        /// <param name="sales">The list of sales that need to be updated or added.</param>
        /// <returns></returns>
        public static async Task<Result> BulkAddOrUpdateSalesAsync(List<Sale> sales)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        for (int i = 0; i < sales.Count; i++)
                        {
                            Sale storedSale = await ctx.Sales.FindAsync(sales[i].ID);
                            if (storedSale == null)
                            {
                                sales[i] = ctx.Sales.Add(sales[i]);
                            }
                            else
                            {
                                ctx.Entry(storedSale).CurrentValues.SetValues(sales[i]);
                            }
                        }

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
        /// Works the same as <see cref="DeleteSaleAsync(Sale)"/>, but doing it for multiple objects at once.
        /// </summary>
        /// <param name="sales">The list of sales that need to be deleted.</param>
        /// <returns></returns>
        public static async Task<Result> BulkDeleteSalesAsync(List<Sale> sales)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        foreach (Sale sale in sales)
                        {
                            ctx.Sales.Attach(sale);
                            ctx.Sales.Remove(sale);
                        }

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
