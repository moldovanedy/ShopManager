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
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                sales = await ctx.Sales.ToListAsync();
            }
            return sales;
        }

        public static async Task<ValueResult<Sale>> AddSaleAsync(Sale sale)
        {
            Sale insertedSale;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                insertedSale = ctx.Sales.Add(sale);

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
                    return ValueResult<Sale>.Failed(saveError);
                }
            }

            return ValueResult<Sale>.Successful(insertedSale);
        }

        public static async Task<Result> RemoveSaleAsync(Sale sale)
        {
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                ctx.Sales.Remove(sale);

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

        public static async Task<ValueResult<Sale>> UpdateSaleAsync(Sale sale)
        {
            Sale updatedSale = null;
            using (AppDBContext ctx = AppDBContext.Instance)
            {
                Error saveError = null;
                await Task.Run(async () =>
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
                });

                if (saveError != null)
                {
                    return ValueResult<Sale>.Failed(saveError);
                }
            }

            return ValueResult<Sale>.Successful(updatedSale);
        }
    }
}
