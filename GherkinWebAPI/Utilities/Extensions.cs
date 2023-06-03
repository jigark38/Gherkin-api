using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Utilities
{
    public static class Extensions
    {
        public static int? IntTryParse(this string num)
        {
            int value;
            if (int.TryParse(num, out value))
            {
                return value;
            }
            return null;
        }

        public static int IntTryParseNotNull(this string num)
        {
            var result = IntTryParse(num);
            return result ?? 0;
        }


        public static decimal? DecimalTryParse(this string num)
        {
            decimal value;
            if (decimal.TryParse(num, out value))
            {
                return value;
            }
            return null;
        }

        public static decimal DecimalTryParseNotNull(this string num)
        {
            var result = DecimalTryParse(num);
            return result ?? 0;
        }

        public static DateTime DateTimeTryParse(this string value)
        {
            DateTime result;
            DateTime.TryParse(value, out result);
            return result;
        }
    }
}