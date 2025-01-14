using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ShopManager.Controller.DataModels;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DBManager;

namespace ShopManager.Controller.DBManager
{
    public static class UsersManager
    {
        public static async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            using (AppDBContext ctx = new AppDBContext())
            {
                users = await ctx.Users.AsNoTracking().ToListAsync();
                if (users.Count == 0)
                {
                    User defaultUser = new User()
                    {
                        Username = "Admin",
                        //hardcoded "root" password
                        Password = "4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2",
                        Permissions = 0b00111111,
                    };
                    ctx.Users.Add(defaultUser);
                    await ctx.SaveChangesAsync();

                    users.Add(defaultUser);
                }
            }
            return users;
        }

        public static async Task<ValueResult<User>> AddUserAsync(User user)
        {
            User insertedUser = null;
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        insertedUser = ctx.Users.Add(user);
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
                return ValueResult<User>.Failed(saveError);
            }

            return ValueResult<User>.Successful(insertedUser);
        }

        public static async Task<Result> DeleteUserAsync(User user)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        ctx.Users.Attach(user);
                        ctx.Users.Remove(user);
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

        public static async Task<Result> AddOrUpdateUserAsync(User user)
        {
            Error saveError = null;
            await Task.Run(async () =>
            {
                using (AppDBContext ctx = new AppDBContext())
                {
                    try
                    {
                        long oldID = user.ID;
                        User storedUser = await ctx.Users.FindAsync(user.ID);
                        if (storedUser == null)
                        {
                            Result addResult = await AddUserAsync(user);
                            saveError = addResult.ResultingError;
                        }
                        else
                        {
                            ctx.Entry(storedUser).CurrentValues.SetValues(user);
                            await ctx.SaveChangesAsync();
                        }
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
