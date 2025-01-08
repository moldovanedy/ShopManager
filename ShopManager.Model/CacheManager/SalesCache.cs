using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopManager.Controller.DBManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Controller.Validation;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.CacheManager
{
    public static class SalesCache
    {
        private static uint _currentPage = 0;
        private static readonly Dictionary<long, Sale> _pageCache = new Dictionary<long, Sale>();
        private static readonly List<long> _modifiedSalesIds = new List<long>();
        private static readonly List<long> _deletedSalesIds = new List<long>();

        /// <summary>
        /// Regenerates (or creates) the cache from the underlying DB at the given page (a page has max. 100 records).
        /// </summary>
        /// <param name="page">The page to get. Giving a nonexistent page will clamp it to the existing limits.</param>
        /// <returns></returns>
        public static async Task RegenerateCacheFromDBAsync(int page = 0)
        {
            _pageCache.Clear();
            _currentPage = (uint)page;
            _modifiedSalesIds.Clear();
            _deletedSalesIds.Clear();

            List<Sale> sales = await SalesManager.GetAllSalesAsync(page * 100, (page * 100) + 100);

            foreach (Sale sale in sales)
            {
                _pageCache.Add(sale.ID, sale);
            }
        }

        /// <summary>
        /// Writes all pending changes to the DB. If it fails, the caller must regenerate the cache from the DB
        /// (as its state is unknown).
        /// </summary>
        /// <returns></returns>
        public static async Task<Result> FlushCacheToDBAsync()
        {
            List<Sale> salesToAddOrUpdate = new List<Sale>();

            foreach (long modID in _modifiedSalesIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_pageCache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as modifiable product.");
                    return Result.Failed(new Error());
                }

                salesToAddOrUpdate.Add(_pageCache[modID]);
            }


            Result operationResult;
            //wait all pending add or update
            operationResult = await SalesManager.BulkAddOrUpdateSalesAsync(salesToAddOrUpdate);
            if (!operationResult.IsSuccess)
            {
                return operationResult;
            }

            for (int i = 0; i < _modifiedSalesIds.Count; i++)
            {
                long updatedID = _pageCache[_modifiedSalesIds[i]].ID;
                if (_pageCache.ContainsKey(updatedID))
                {
                    _pageCache[updatedID] = _pageCache[_modifiedSalesIds[i]];
                }
                else
                {
                    _pageCache.Add(updatedID, _pageCache[_modifiedSalesIds[i]]);
                    _pageCache.Remove(_modifiedSalesIds[i]);
                }
            }
            _modifiedSalesIds.Clear();

            List<Sale> salesToDelete = new List<Sale>();

            foreach (long modID in _deletedSalesIds)
            {
                //if one is invalid, wait for all pending updates, then return the error
                if (!_pageCache.ContainsKey(modID))
                {
                    Logger.LogError($"Cache did not contain {modID}, but was presented as deletable product.");
                    return Result.Failed(new Error());
                }

                salesToDelete.Add(_pageCache[modID]);
            }

            //wait all pending delete
            operationResult = await SalesManager.BulkDeleteSalesAsync(salesToDelete);
            if (!operationResult.IsSuccess)
            {
                return operationResult;
            }

            for (int i = 0; i < _deletedSalesIds.Count; i++)
            {
                _pageCache.Remove(_deletedSalesIds[i]);
            }
            _deletedSalesIds.Clear();

            return Result.Successful();
        }

        public static uint GetCurrentPage()
        {
            return _currentPage;
        }

        public static int GetNumberOfSalesOnCurrentPage()
        {
            return _pageCache.Count;
        }

        /// <summary>
        /// Will return the UNSORTED sales from the current page.
        /// </summary>
        /// <returns></returns>
        public static List<Sale> GetAllSalesFromCurrentPage()
        {
            List<Sale> sales = new List<Sale>();
            foreach (KeyValuePair<long, Sale> pair in _pageCache)
            {
                if (_deletedSalesIds.Contains(pair.Key))
                {
                    continue;
                }

                pair.Value.ID = pair.Key;
                sales.Add(pair.Value);
            }

            return sales;
        }

        public static ValueResult<Sale> GetSale(long id)
        {
            if (_deletedSalesIds.Contains(id))
            {
                return ValueResult<Sale>.Failed(new Error());
            }

            if (_pageCache.TryGetValue(id, out Sale sale))
            {
                return ValueResult<Sale>.Successful(sale);
            }
            else
            {
                return ValueResult<Sale>.Failed(new Error());
            }
        }

        /// <summary>
        /// Adds a sale to the cache so it can later be written to the DB.
        /// </summary>
        /// <param name="sale">The sale to add.</param>
        /// <param name="isIncomplete">
        /// If true, will NOT validate all the sale fields, important when the sale is added from the table directly.
        /// If false, will validate all the fields of the sale, returning an error if something is wrong.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the sale is null.</exception>
        public static Result AddSale(Sale sale, bool isIncomplete = false)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            if (_pageCache.ContainsKey(sale.ID))
            {
                return Result.Failed(new Error());
            }

            if (sale.ID == 0)
            {
                sale.ID = DummyIDGenerator.GetDummyID();
            }

            if (!isIncomplete)
            {
                Result validationResult = SaleValidations.ValidateEntireSale(sale);
                if (!validationResult.IsSuccess)
                {
                    return Result.Failed(validationResult.ResultingError);
                }
            }

            _pageCache.Add(sale.ID, sale);

            if (!_modifiedSalesIds.Contains(sale.ID))
            {
                _modifiedSalesIds.Add(sale.ID);
            }
            return Result.Successful();
        }

        public static Result DeleteSale(long id)
        {
            if (_pageCache.ContainsKey(id))
            {
                if (!_deletedSalesIds.Contains(id))
                {
                    if (_modifiedSalesIds.Contains(id))
                    {
                        //if it was a just added sale (not yet saved), just remove it from adding and from the list;
                        //but if it is an updated one, we need to delete it, so add it to the delete list
                        if (id < long.MaxValue - 1000)
                        {
                            _deletedSalesIds.Add(id);
                        }
                        else
                        {
                            //the added sale is in the cache, delete it
                            _pageCache.Remove(id);
                        }

                        _modifiedSalesIds.Remove(id);
                    }
                    else
                    {
                        _deletedSalesIds.Add(id);
                    }
                }
                return Result.Successful();
            }
            else
            {
                return Result.Failed(new Error());
            }
        }

        public static Result UpdateSale(Sale sale, bool isIncomplete = false)
        {
            if (sale == null)
            {
                return Result.Failed(new Error());
            }

            if (!_pageCache.ContainsKey(sale.ID))
            {
                return Result.Failed(new Error());
            }

            if (sale.ID == 0)
            {
                sale.ID = DummyIDGenerator.GetDummyID();
            }

            if (!isIncomplete)
            {
                Result validationResult = SaleValidations.ValidateEntireSale(sale);
                if (!validationResult.IsSuccess)
                {
                    return Result.Failed(validationResult.ResultingError);
                }
            }

            _pageCache[sale.ID] = sale;

            if (!_modifiedSalesIds.Contains(sale.ID))
            {
                _modifiedSalesIds.Add(sale.ID);
            }
            return Result.Successful();
        }
    }
}
