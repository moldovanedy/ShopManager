using System;
using System.Globalization;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.Validation
{
    public static class ProductValidations
    {
        public static Result ValidateEntireProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            for (int i = 1; i < 9; i++)
            {
                ValueResult<string> valueResult = null;
                switch (i)
                {
                    case 1:
                        valueResult = ValidateName(product.Name);
                        break;
                    case 2:
                        valueResult = ValidateDescription(product.Description);
                        break;
                    case 3:
                        valueResult = ValidatePrice(product.Price.ToString(), out _);
                        break;
                    case 4:
                        valueResult = ValidatePrice(product.PricePerKg.ToString(), out _);
                        break;
                    case 5:
                        valueResult = ValidateDate(product.PurchaseDate.ToString(), out _);
                        break;
                    case 6:
                        valueResult = ValidateDate(product.ExpiryDate.ToString(), out _);
                        break;
                    case 7:
                        valueResult = ValidateQuantity(product.Quantity.ToString(), out _);
                        break;
                    case 8:
                        ValueResult<ProductCategory> categoryResult =
                            CategoriesCache.GetCategory(product.CategoryID);
                        valueResult =
                            categoryResult.IsSuccess ?
                                ValueResult<string>.Successful(categoryResult.Value.Name) :
                                ValueResult<string>.Failed(new Error());
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

        public static ValueResult<string> ValidateName(string unverifiedValue)
        {
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                return ValueResult<string>.Failed(new Error());
            }

            if (unverifiedValue.Length > byte.MaxValue)
            {
                return ValueResult<string>.Failed(new Error());
            }

            return ValueResult<string>.Successful(unverifiedValue);
        }

        public static ValueResult<string> ValidateDescription(string unverifiedValue)
        {
            if (unverifiedValue.Length > ushort.MaxValue)
            {
                return ValueResult<string>.Failed(new Error());
            }

            return ValueResult<string>.Successful(unverifiedValue);
        }

        public static ValueResult<string> ValidatePrice(string unverifiedValue, out double resultingPrice)
        {
            resultingPrice = double.NaN;
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                resultingPrice = 0.0;
                return ValueResult<string>.Successful("");
            }

            //handle differences between locales (decimal separator being "." or ","
            unverifiedValue = NumberGlobalizationHandler.GlobalizeNumericString(unverifiedValue);

            if (double.TryParse(
                unverifiedValue,
                NumberStyles.Currency,
                CultureInfo.InvariantCulture,
                out double price))
            {
                resultingPrice = price;
                return ValueResult<string>.Successful(unverifiedValue);
            }
            else
            {
                return ValueResult<string>.Failed(new Error());
            }
        }

        public static ValueResult<string> ValidateDate(string unverifiedValue, out DateTime resultingDateTime)
        {
            if (DateTime.TryParse(unverifiedValue, out resultingDateTime))
            {
                return ValueResult<string>.Successful(unverifiedValue);
            }
            else
            {
                resultingDateTime = DateTime.MinValue;
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
