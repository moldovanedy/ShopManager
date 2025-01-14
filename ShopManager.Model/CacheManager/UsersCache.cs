using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.DataModels;
using ShopManager.Controller.DBManager;
using ShopManager.Controller.ResultHandler;

namespace ShopManager.Controller.CacheManager
{
    public static class UsersCache
    {
        private static readonly Dictionary<long, User> _cache = new Dictionary<long, User>();
        private static readonly List<long> _modifiedUsersIds = new List<long>();
        private static readonly List<long> _deletedUsersIds = new List<long>();

        public static async Task RegenerateCacheFromDBAsync()
        {
            _cache.Clear();
            _modifiedUsersIds.Clear();
            _deletedUsersIds.Clear();

            List<User> users = await UsersManager.GetAllUsersAsync();

            foreach (User user in users)
            {
                _cache.Add(user.ID, user);
            }
        }

        /// <summary>
        /// Writes all pending changes to the DB. If it fails, the caller must regenerate the cache from the DB
        /// (as its state is unknown).
        /// </summary>
        /// <returns></returns>
        public static async Task<Result> FlushCacheToDBAsync()
        {
            List<Task<Result>> updateOperations = new List<Task<Result>>();

            foreach (long modID in _modifiedUsersIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as modifiable product.");

                    await Task.WhenAll(updateOperations);
                    return Result.Failed(new Error());
                }

                updateOperations.Add(UsersManager.AddOrUpdateUserAsync(_cache[modID]));
            }

            //wait all pending add or update
            await Task.WhenAll(updateOperations);

            bool allSuccessful = true;
            for (int i = 0; i < updateOperations.Count; i++)
            {
                if (!updateOperations[i].Result.IsSuccess)
                {
                    allSuccessful = false;
                    break;
                }

                long updatedID = _cache[_modifiedUsersIds[i]].ID;
                if (_cache.ContainsKey(updatedID))
                {
                    _cache[updatedID] = _cache[_modifiedUsersIds[i]];
                }
                else
                {
                    _cache.Add(updatedID, _cache[_modifiedUsersIds[i]]);
                    _cache.Remove(_modifiedUsersIds[i]);
                }
            }
            _modifiedUsersIds.Clear();

            if (!allSuccessful)
            {
                return Result.Failed(new Error());
            }


            List<Task<Result>> deleteOperations = new List<Task<Result>>();

            foreach (long modID in _deletedUsersIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as deletable product.");

                    await Task.WhenAll(deleteOperations);
                    return Result.Failed(new Error());
                }

                deleteOperations.Add(UsersManager.DeleteUserAsync(_cache[modID]));
            }

            //wait all pending delete
            await Task.WhenAll(deleteOperations);

            bool allDeletionsSuccessful = true;
            for (int i = 0; i < deleteOperations.Count; i++)
            {
                if (!deleteOperations[i].Result.IsSuccess)
                {
                    allDeletionsSuccessful = false;
                    break;
                }

                _cache.Remove(_deletedUsersIds[i]);
            }
            _deletedUsersIds.Clear();

            return allDeletionsSuccessful ? Result.Successful() : Result.Failed(new Error());
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            foreach (KeyValuePair<long, User> pair in _cache)
            {
                if (_deletedUsersIds.Contains(pair.Key))
                {
                    continue;
                }

                pair.Value.ID = pair.Key;
                users.Add(pair.Value);
            }

            return users;
        }

        public static ValueResult<User> GetUser(long id)
        {
            if (_cache.TryGetValue(id, out User product))
            {
                return ValueResult<User>.Successful(product);
            }
            else
            {
                return ValueResult<User>.Failed(new Error());
            }
        }

        /// <summary>
        /// Search the users and returns the one that has the given name if it exists, a failed result if not.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A successful result containing the user if it is found, a failed result otherwise.</returns>
        /// <exception cref="ArgumentException">If the given string is null.</exception>
        public static ValueResult<User> SearchSingleUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentException("Parameter is null", nameof(username));
            }

            User foundUser = null;
            foreach (KeyValuePair<long, User> userPair in _cache)
            {
                if (userPair.Value.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                {
                    foundUser = userPair.Value;
                    break;
                }
            }

            //it means it hasn't found an user
            if (foundUser == null)
            {
                return ValueResult<User>.Failed(new Error());
            }

            return ValueResult<User>.Successful(foundUser);
        }

        /// <summary>
        /// Search the users and returns a list of users that contain the search string in their username,
        /// a failed result if not.
        /// </summary>
        /// <param name="searchString">The name to search for.</param>
        /// <returns>
        /// A successful result containing the list of users if any are found, a failed result otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">If the given string is null.</exception>
        public static ValueResult<List<User>> SearchUsers(string searchString)
        {
            if (searchString == null)
            {
                throw new ArgumentException("Parameter is null", nameof(searchString));
            }

            List<User> users =
                _cache
                    .Where((userPair) =>
                    {
                        return userPair.Value.Username.Contains(searchString);
                    })
                    .Select((pair) => pair.Value)
                    .ToList();

            if (users.Count == 0)
            {
                return ValueResult<List<User>>.Failed(new Error());
            }
            else
            {
                return ValueResult<List<User>>.Successful(users);
            }
        }

        /// <summary>
        /// Adds a user to the cache so it can later be written to the DB.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the user is null.</exception>
        public static Result AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (_cache.ContainsKey(user.ID))
            {
                return Result.Failed(new Error());
            }

            if (_cache.Where(prod => prod.Value.Username == user.Username).Any())
            {
                return Result.Failed(new Error());
            }

            if (user.ID == 0)
            {
                user.ID = DummyIDGenerator.GetDummyID();
            }

            _cache.Add(user.ID, user);

            if (!_modifiedUsersIds.Contains(user.ID))
            {
                _modifiedUsersIds.Add(user.ID);
            }
            return Result.Successful();
        }

        public static Result DeleteUser(long id)
        {
            if (_cache.ContainsKey(id))
            {
                if (!_deletedUsersIds.Contains(id))
                {
                    if (_modifiedUsersIds.Contains(id))
                    {
                        //if it was a just added user (not yet saved), just remove it from adding and from the list;
                        //but if it is an updated one, we need to delete it, so add it to the delete list
                        if (id < long.MaxValue - 1000)
                        {
                            _deletedUsersIds.Add(id);
                        }
                        else
                        {
                            //the added user is in the cache, delete it
                            _cache.Remove(id);
                        }

                        _modifiedUsersIds.Remove(id);
                    }
                    else
                    {
                        _deletedUsersIds.Add(id);
                    }
                }
                return Result.Successful();
            }
            else
            {
                return Result.Failed(new Error());
            }
        }

        public static Result UpdateUser(User user)
        {
            if (user == null)
            {
                return Result.Failed(new Error());
            }

            if (!_cache.ContainsKey(user.ID))
            {
                return Result.Failed(new Error());
            }

            //if there is a user that has the same name, but a different ID it is a duplicate and it's not allowed
            if (_cache
                .Where(
                    prod =>
                        prod.Value.Username == user.Username &&
                        prod.Value.ID != user.ID)
                .Any())
            {
                return Result.Failed(new Error());
            }

            if (user.ID == 0)
            {
                user.ID = DummyIDGenerator.GetDummyID();
            }
            _cache[user.ID] = user;

            if (!_modifiedUsersIds.Contains(user.ID))
            {
                _modifiedUsersIds.Add(user.ID);
            }
            return Result.Successful();
        }
    }
}
