using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.DBManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.CacheManager
{
    public static class CategoriesCache
    {
        private static readonly Dictionary<long, ProductCategory> _cache = new Dictionary<long, ProductCategory>();
        private static readonly List<long> _modifiedCategoriesIds = new List<long>();
        private static readonly List<long> _deletedCategoriesIds = new List<long>();

        public static async Task RegenerateCacheFromDBAsync()
        {
            _cache.Clear();
            _modifiedCategoriesIds.Clear();
            _deletedCategoriesIds.Clear();

            List<ProductCategory> categories = await CategoriesManager.GetAllCategoriesAsync();

            foreach (ProductCategory category in categories)
            {
                _cache.Add(category.ID, category);
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

            foreach (long modID in _modifiedCategoriesIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as modifiable product.");

                    await Task.WhenAll(updateOperations);
                    return Result.Failed(new Error());
                }

                updateOperations.Add(CategoriesManager.AddOrUpdateCategoryAsync(_cache[modID]));
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

                long updatedID = _cache[_modifiedCategoriesIds[i]].ID;
                if (_cache.ContainsKey(updatedID))
                {
                    _cache[updatedID] = _cache[_modifiedCategoriesIds[i]];
                }
                else
                {
                    _cache.Add(updatedID, _cache[_modifiedCategoriesIds[i]]);
                    _cache.Remove(_modifiedCategoriesIds[i]);
                }
            }
            _modifiedCategoriesIds.Clear();

            if (!allSuccessful)
            {
                return Result.Failed(new Error());
            }


            List<Task<Result>> deleteOperations = new List<Task<Result>>();

            foreach (long modID in _deletedCategoriesIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as deletable product.");

                    await Task.WhenAll(deleteOperations);
                    return Result.Failed(new Error());
                }

                deleteOperations.Add(CategoriesManager.DeleteCategoryAsync(_cache[modID]));
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

                _cache.Remove(_deletedCategoriesIds[i]);
            }
            _deletedCategoriesIds.Clear();

            return allDeletionsSuccessful ? Result.Successful() : Result.Failed(new Error());
        }

        public static List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            foreach (KeyValuePair<long, ProductCategory> pair in _cache)
            {
                if (_deletedCategoriesIds.Contains(pair.Key))
                {
                    continue;
                }

                pair.Value.ID = pair.Key;
                categories.Add(pair.Value);
            }

            return categories;
        }

        public static ValueResult<ProductCategory> GetCategory(long id)
        {
            if (_cache.TryGetValue(id, out ProductCategory product))
            {
                return ValueResult<ProductCategory>.Successful(product);
            }
            else
            {
                return ValueResult<ProductCategory>.Failed(new Error());
            }
        }

        /// <summary>
        /// Search the categories and returns the one that has the given name if it exists, a failed result if not.
        /// </summary>
        /// <param name="categoryName">The name to search for.</param>
        /// <returns>A successful result containing the category if it is found, a failed result otherwise.</returns>
        /// <exception cref="ArgumentException">If the given string is null.</exception>
        public static ValueResult<ProductCategory> SearchSingleCategory(string categoryName)
        {
            if (categoryName == null)
            {
                throw new ArgumentException("Parameter is null", nameof(categoryName));
            }

            ProductCategory foundCategory = null;
            foreach (KeyValuePair<long, ProductCategory> categoryPair in _cache)
            {
                if (categoryPair.Value.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase))
                {
                    foundCategory = categoryPair.Value;
                    break;
                }
            }

            //it means it hasn't found a category
            if (foundCategory == null)
            {
                return ValueResult<ProductCategory>.Failed(new Error());
            }

            return ValueResult<ProductCategory>.Successful(foundCategory);
        }

        /// <summary>
        /// Search the categories and returns a list of categories that contain the search string in their name,
        /// a failed result if not.
        /// </summary>
        /// <param name="searchString">The name to search for.</param>
        /// <returns>
        /// A successful result containing the list of categories if any are found, a failed result otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">If the given string is null.</exception>
        public static ValueResult<List<ProductCategory>> SearchCategories(string searchString)
        {
            if (searchString == null)
            {
                throw new ArgumentException("Parameter is null", nameof(searchString));
            }

            List<ProductCategory> categories =
                _cache
                    .Where((categoryPair) =>
                    {
                        return categoryPair.Value.Name.Contains(searchString);
                    })
                    .Select((pair) => pair.Value)
                    .ToList();

            if (categories.Count == 0)
            {
                return ValueResult<List<ProductCategory>>.Failed(new Error());
            }
            else
            {
                return ValueResult<List<ProductCategory>>.Successful(categories);
            }
        }

        /// <summary>
        /// Adds a category to the cache so it can later be written to the DB.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the category is null.</exception>
        public static Result AddCategory(ProductCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (_cache.ContainsKey(category.ID))
            {
                return Result.Failed(new Error());
            }

            if (_cache.Where(prod => prod.Value.Name == category.Name).Any())
            {
                return Result.Failed(new Error());
            }

            if (category.ID == 0)
            {
                category.ID = DummyIDGenerator.GetDummyID();
            }

            //Result validationResult = ProductValidations.ValidateEntireProduct(product);
            //if (!validationResult.IsSuccess)
            //{
            //    return Result.Failed(validationResult.ResultingError);
            //}

            _cache.Add(category.ID, category);

            if (!_modifiedCategoriesIds.Contains(category.ID))
            {
                _modifiedCategoriesIds.Add(category.ID);
            }
            return Result.Successful();
        }

        public static Result DeleteCategory(long id)
        {
            if (_cache.ContainsKey(id))
            {
                if (!_deletedCategoriesIds.Contains(id))
                {
                    if (_modifiedCategoriesIds.Contains(id))
                    {
                        //if it was a just added category (not yet saved), just remove it from adding and from the list;
                        //but if it is an updated one, we need to delete it, so add it to the delete list
                        if (id < long.MaxValue - 1000)
                        {
                            _deletedCategoriesIds.Add(id);
                        }
                        else
                        {
                            //the added category is in the cache, delete it
                            _cache.Remove(id);
                        }

                        _modifiedCategoriesIds.Remove(id);
                    }
                    else
                    {
                        _deletedCategoriesIds.Add(id);
                    }
                }
                return Result.Successful();
            }
            else
            {
                return Result.Failed(new Error());
            }
        }

        public static Result UpdateCategory(ProductCategory category)
        {
            if (category == null)
            {
                return Result.Failed(new Error());
            }

            if (!_cache.ContainsKey(category.ID))
            {
                return Result.Failed(new Error());
            }

            //if there is a category that has the same name, but a different ID it is a duplicate and it's not allowed
            if (_cache
                .Where(
                    prod =>
                        prod.Value.Name == category.Name &&
                        prod.Value.ID != category.ID)
                .Any())
            {
                return Result.Failed(new Error());
            }

            if (category.ID == 0)
            {
                category.ID = DummyIDGenerator.GetDummyID();
            }

            //Result validationResult = ProductValidations.ValidateEntireProduct(category);
            //if (!validationResult.IsSuccess)
            //{
            //    return Result.Failed(validationResult.ResultingError);
            //}

            _cache[category.ID] = category;

            if (!_modifiedCategoriesIds.Contains(category.ID))
            {
                _modifiedCategoriesIds.Add(category.ID);
            }
            return Result.Successful();
        }
    }
}
