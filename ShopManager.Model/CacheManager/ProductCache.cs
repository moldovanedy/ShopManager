using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopManager.Controller.DBManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Controller.Validation;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.CacheManager
{
    public static class ProductCache
    {
        private static readonly Dictionary<long, Product> _cache = new Dictionary<long, Product>();
        private static readonly List<long> _modifiedProductIds = new List<long>();
        private static readonly List<long> _deletedProductsIds = new List<long>();

        /// <summary>
        /// Regenerates (or creates) the cache from the underlying DB.
        /// </summary>
        /// <returns></returns>
        public static async Task RegenerateCacheFromDBAsync()
        {
            _cache.Clear();
            _modifiedProductIds.Clear();
            _deletedProductsIds.Clear();

            List<Product> products = await ProductsManager.GetAllProductsAsync();

            foreach (Product product in products)
            {
                _cache.Add(product.ID, product);
            }
        }

        /// <summary>
        /// Writes all pending changes to the DB. If it fails, the caller must regenerate the cache from the DB
        /// (as its state is unknown).
        /// </summary>
        /// <returns></returns>
        public static async Task<Result> FlushCacheToDBAsync()
        {
            List<Product> productsToAddOrUpdate = new List<Product>();

            foreach (long modID in _modifiedProductIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as modifiable product.");
                    return Result.Failed(new Error());
                }

                productsToAddOrUpdate.Add(_cache[modID]);
            }

            Result operationResult;
            //wait all pending add or update
            operationResult = await ProductsManager.BulkAddOrUpdateProductsAsync(productsToAddOrUpdate);
            if (!operationResult.IsSuccess)
            {
                return operationResult;
            }

            for (int i = 0; i < _modifiedProductIds.Count; i++)
            {
                long updatedID = _cache[_modifiedProductIds[i]].ID;
                if (_cache.ContainsKey(updatedID))
                {
                    _cache[updatedID] = _cache[_modifiedProductIds[i]];
                }
                else
                {
                    _cache.Add(updatedID, _cache[_modifiedProductIds[i]]);
                    _cache.Remove(_modifiedProductIds[i]);
                }
            }
            _modifiedProductIds.Clear();

            List<Product> productsToDelete = new List<Product>();

            foreach (long modID in _deletedProductsIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_cache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as deletable product.");
                    return Result.Failed(new Error());
                }

                productsToDelete.Add(_cache[modID]);
            }

            //wait all pending delete
            operationResult = await ProductsManager.BulkDeleteProductsAsync(productsToDelete);
            if (!operationResult.IsSuccess)
            {
                return operationResult;
            }

            for (int i = 0; i < _deletedProductsIds.Count; i++)
            {
                _cache.Remove(_deletedProductsIds[i]);
            }
            _deletedProductsIds.Clear();

            return Result.Successful();
        }

        public static int GetNumberOfProducts()
        {
            return _cache.Count;
        }

        /// <summary>
        /// Will return the UNSORTED products.
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetAllProducts(string searchString = "")
        {
            List<Product> products = new List<Product>();
            foreach (KeyValuePair<long, Product> pair in _cache)
            {
                if (_deletedProductsIds.Contains(pair.Key))
                {
                    continue;
                }

                //search
                if (
                    searchString != "" &&
                    !pair.Value.Name.ToLower().Contains(searchString.ToLower()))
                {
                    continue;
                }

                pair.Value.ID = pair.Key;
                products.Add(pair.Value);
            }

            return products;
        }

        public static ValueResult<Product> GetProduct(long id)
        {
            if (_deletedProductsIds.Contains(id))
            {
                return ValueResult<Product>.Failed(new Error());
            }

            if (_cache.TryGetValue(id, out Product product))
            {
                return ValueResult<Product>.Successful(product);
            }
            else
            {
                return ValueResult<Product>.Failed(new Error());
            }
        }

        /// <summary>
        /// Search the products and returns the one that has the given name if it exists, a failed result if not.
        /// </summary>
        /// <param name="productName">The name to search for.</param>
        /// <returns>A successful result containing the product if it is found, a failed result otherwise.</returns>
        /// <exception cref="ArgumentException">If the given string is null or empty.</exception>
        public static ValueResult<Product> SearchSingleProduct(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentException("Parameter is null or empty", nameof(productName));
            }

            Product foundProduct = null;
            foreach (KeyValuePair<long, Product> productPair in _cache)
            {
                if (productPair.Value.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase) &&
                    !_deletedProductsIds.Contains(productPair.Key))
                {
                    foundProduct = productPair.Value;
                    break;
                }
            }

            //it means it hasn't found a product
            if (foundProduct == null)
            {
                return ValueResult<Product>.Failed(new Error());
            }

            return ValueResult<Product>.Successful(foundProduct);
        }

        /// <summary>
        /// Search the products and returns a list of products that contain the search string in their name,
        /// a failed result if not.
        /// </summary>
        /// <param name="searchString">The name to search for.</param>
        /// <returns>
        /// A successful result containing the list of products if any are found, a failed result otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">If the given string is null or empty.</exception>
        public static ValueResult<List<Product>> SearchProducts(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                throw new ArgumentException("Parameter is null or empty", nameof(searchString));
            }

            List<Product> products =
                _cache
                    .Where((prodPair) =>
                    {
                        return
                            prodPair.Value.Name.Contains(searchString) &&
                            !_deletedProductsIds.Contains(prodPair.Key);
                    })
                    .Select((pair) => pair.Value)
                    .ToList();

            if (products.Count == 0)
            {
                return ValueResult<List<Product>>.Failed(new Error());
            }
            else
            {
                return ValueResult<List<Product>>.Successful(products);
            }
        }

        /// <summary>
        /// Adds a product to the cache so it can later be written to the DB.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <param name="isIncomplete">
        /// If true, will NOT validate all the product fields, important when the product is added from the table directly.
        /// If false, will validate all the fields of the product, returning an error if something is wrong.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the product is null.</exception>
        public static Result AddProduct(Product product, bool isIncomplete = false)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (_cache.ContainsKey(product.ID))
            {
                return Result.Failed(new Error());
            }

            if (_cache.Where(prod => prod.Value.Name == product.Name).Any())
            {
                return Result.Failed(new Error());
            }

            if (product.ID == 0)
            {
                product.ID = DummyIDGenerator.GetDummyID();
            }

            if (!isIncomplete)
            {
                Result validationResult = ProductValidations.ValidateEntireProduct(product);
                if (!validationResult.IsSuccess)
                {
                    return Result.Failed(validationResult.ResultingError);
                }
            }

            _cache.Add(product.ID, product);

            if (!_modifiedProductIds.Contains(product.ID))
            {
                _modifiedProductIds.Add(product.ID);
            }
            return Result.Successful();
        }

        public static Result DeleteProduct(long id)
        {
            if (_cache.ContainsKey(id))
            {
                if (!_deletedProductsIds.Contains(id))
                {
                    if (_modifiedProductIds.Contains(id))
                    {
                        //if it was a just added product (not yet saved), just remove it from adding and from the list;
                        //but if it is an updated one, we need to delete it, so add it to the delete list
                        if (id < long.MaxValue - 1000)
                        {
                            _deletedProductsIds.Add(id);
                        }
                        else
                        {
                            //the added product is in the cache, delete it
                            _cache.Remove(id);
                        }

                        _modifiedProductIds.Remove(id);
                    }
                    else
                    {
                        _deletedProductsIds.Add(id);
                    }
                }
                return Result.Successful();
            }
            else
            {
                return Result.Failed(new Error());
            }
        }

        public static Result UpdateProduct(Product product, bool isIncomplete = false)
        {
            if (product == null)
            {
                return Result.Failed(new Error());
            }

            if (!_cache.ContainsKey(product.ID))
            {
                return Result.Failed(new Error());
            }

            //if there is a product that has the same name, but a different ID it is a duplicate and it's not allowed
            if (_cache
                .Where(
                    prod =>
                        prod.Value.Name == product.Name &&
                        prod.Value.ID != product.ID)
                .Any())
            {
                return Result.Failed(new Error());
            }

            if (product.ID == 0)
            {
                product.ID = DummyIDGenerator.GetDummyID();
            }

            if (!isIncomplete)
            {
                Result validationResult = ProductValidations.ValidateEntireProduct(product);
                if (!validationResult.IsSuccess)
                {
                    return Result.Failed(validationResult.ResultingError);
                }
            }

            _cache[product.ID] = product;

            if (!_modifiedProductIds.Contains(product.ID))
            {
                _modifiedProductIds.Add(product.ID);
            }
            return Result.Successful();
        }
    }
}
