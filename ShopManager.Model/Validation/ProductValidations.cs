using System;
using System.Globalization;
using System.Linq;
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
                        valueResult = ValueResult<string>.Successful("Fructe");
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
                return ValueResult<string>.Failed(new Error());
            }

            //handle differences between locales (decimal separator being "." or ","
            bool encounteredDigitalSeparator = false;
            char decimalSeparator = '.';
            char[] newString = new char[unverifiedValue.Length];
            for (
                int i = unverifiedValue.Length - 1, newStringCurrentIndex = i;
                i >= 0; i--,
                newStringCurrentIndex--)
            {
                //if the decimal separator is encountered, override the format preferences
                //to allow both ',' and '.' as decimal separators so it will work regardless of the locale
                //(for simple expressions, such as 12345.67 or 12345,67)
                if (!encounteredDigitalSeparator && (unverifiedValue[i] == ',' || unverifiedValue[i] == '.'))
                {
                    if (unverifiedValue[i] == '.')
                    {
                        decimalSeparator = '.';
                    }
                    else if (unverifiedValue[i] == ',')
                    {
                        decimalSeparator = ',';
                    }

                    encounteredDigitalSeparator = true;
                }

                //convert to the international formatting rules
                if (decimalSeparator == unverifiedValue[i])
                {
                    newString[newStringCurrentIndex] = '.';
                    continue;
                }
                //don't include any kind of digit separators (the decimal separator will be handled by the above if)
                if (unverifiedValue[i] < '0' || unverifiedValue[i] > '9')
                {
                    newStringCurrentIndex++;
                    continue;
                }

                newString[newStringCurrentIndex] = unverifiedValue[i];
            }
            unverifiedValue = new string(newString.ToArray());

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
            quantity = 0;
            if (string.IsNullOrEmpty(unverifiedValue))
            {
                return ValueResult<string>.Failed(new Error());
            }

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
