using System.Linq;

namespace ShopManager.Controller
{
    public static class NumberGlobalizationHandler
    {
        /// <summary>
        /// Converts the given string to the international format ("." as decimal separator) for parsing methods.
        /// </summary>
        /// <param name="rawValue"></param>
        /// <returns></returns>
        public static string GlobalizeNumericString(string rawValue)
        {
            bool encounteredDigitalSeparator = false;
            char decimalSeparator = '.';
            char[] newString = new char[rawValue.Length];
            for (
                int i = rawValue.Length - 1, newStringCurrentIndex = i;
                i >= 0;
                i--, newStringCurrentIndex--)
            {
                //if the decimal separator is encountered, override the format preferences
                //to allow both ',' and '.' as decimal separators so it will work regardless of the locale
                //(for simple expressions, such as 12345.67 or 12345,67)
                if (!encounteredDigitalSeparator && (rawValue[i] == ',' || rawValue[i] == '.'))
                {
                    if (rawValue[i] == '.')
                    {
                        decimalSeparator = '.';
                    }
                    else if (rawValue[i] == ',')
                    {
                        decimalSeparator = ',';
                    }

                    encounteredDigitalSeparator = true;
                }

                //consider the sign
                if (i == 0 && (rawValue[i] == '+' || rawValue[i] == '-'))
                {
                    newString[newStringCurrentIndex] = rawValue[i];
                    break;
                }

                //convert to the international formatting rules
                if (decimalSeparator == rawValue[i])
                {
                    newString[newStringCurrentIndex] = '.';
                    continue;
                }

                //don't include any kind of digit separators (the decimal separator will be handled by the above if)
                if (rawValue[i] < '0' || rawValue[i] > '9')
                {
                    //to overwrite the upcoming --
                    newStringCurrentIndex++;
                    continue;
                }

                newString[newStringCurrentIndex] = rawValue[i];
            }
            rawValue = new string(newString.ToArray());

            return rawValue;
        }
    }
}
