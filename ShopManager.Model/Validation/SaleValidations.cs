using System;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.Validation
{
    public static class SaleValidations
    {
        public static Result ValidateEntireSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            for (int i = 1; i < 3; i++)
            {
                ValueResult<string> valueResult = null;
                switch (i)
                {
                    case 1:
                        valueResult = ValidateProductID(sale.ProductID.ToString());
                        break;
                    case 2:
                        valueResult = ValidateQuantity(sale.Quantity.ToString(), out _);
                        break;
                    default:
                        break;
                }

                if (!valueResult.IsSuccess)
                {
                    return Result.Failed(valueResult.ResultingError);
                }
            }

            return Result.Successful();
        }

        public static ValueResult<string> ValidateProductID(string unverifiedValue)
        {
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                return ValueResult<string>.Failed(new Error());
            }
            if (!long.TryParse(unverifiedValue, out long id))
            {
                return ValueResult<string>.Failed(new Error());
            }

            ValueResult<Product> productResult = ProductCache.GetProduct(id);
            if (productResult.IsSuccess)
            {
                return ValueResult<string>.Successful(unverifiedValue);
            }
            else
            {
                return ValueResult<string>.Failed(new Error());
            }
        }

        public static ValueResult<string> ValidateProductName(string unverifiedValue)
        {
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                return ValueResult<string>.Failed(new Error());
            }

            if (ProductCache.SearchSingleProduct(unverifiedValue).IsSuccess)
            {
                return ValueResult<string>.Successful(unverifiedValue);
            }
            else
            {
                return ValueResult<string>.Failed(new Error());
            }
        }

        public static ValueResult<string> ValidateQuantity(string unverifiedValue, out double quantity)
        {
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                quantity = 0.0;
                return ValueResult<string>.Successful("");
            }

            //handle differences between locales (decimal separator being "." or ","
            unverifiedValue = NumberGlobalizationHandler.GlobalizeNumericString(unverifiedValue);

            if (double.TryParse(unverifiedValue, out quantity))
            {
                return ValueResult<string>.Successful(unverifiedValue);
            }
            else
            {
                return ValueResult<string>.Failed(new Error());
            }
        }
    }
}
