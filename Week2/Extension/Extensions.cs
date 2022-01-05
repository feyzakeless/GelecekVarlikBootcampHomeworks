using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Extension
{
    public static class Extensions
    {
        //Get Enum
        public static string GetEnum(this Enum _variable)
        {
            var result = _variable.GetType().GetMember(_variable.ToString()).First().GetCustomAttributes<DisplayAttribute>().First().Name + "-" +
                _variable.GetType().GetMember(_variable.ToString()).First().GetCustomAttributes<DisplayAttribute>().First().GroupName;
            return result;
        }


        //Create Extension
        public static string ConvertToEuro(this int item)
        {
            return "1 EURO = " + item * 15.55 + " TRY";
        }

        public static string ConvertToUsd(this int item)
        {
            return "1 USD = " + item * 13.74 + " TRY";
        }
    }
}
