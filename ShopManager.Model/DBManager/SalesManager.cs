using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (AppDBContext ctx = new AppDBContext())
            {
                sales = await ctx.Sales.ToListAsync();
            }
            return sales;
        }

        public static async Task<ValueResult<Sale>> AddSaleAsync(Sale sale)
        {
            Sale insertedSale = null;

            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    insertedSale = ctx.Sales.Add(sale);

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
                return ValueResult<Sale>.Failed(saveError);
            }

            return ValueResult<Sale>.Successful(insertedSale);
        }

        public static async Task<Result> RemoveSaleAsync(Sale sale)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    ctx.Sales.Remove(sale);

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

        public static async Task<ValueResult<Sale>> UpdateSaleAsync(Sale sale)
        {
            Sale updatedSale = null;
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        Sale storedSale = ctx.Sales.Find(sale.ID);
                        if (storedSale == null)
                        {
                            updatedSale = sale;
                            return;
                        }

                        ctx.Entry(storedSale).CurrentValues.SetValues(sale);

                        updatedSale = storedSale;
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

            return ValueResult<Sale>.Successful(updatedSale);
        }
    }
}
