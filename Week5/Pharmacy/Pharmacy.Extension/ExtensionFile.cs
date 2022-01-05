using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Pharmacy.Extension
{
    public static class ExtensionFile
    {
        //Enum in type i ve attribute u alindi
        public static string GetEnum(this Enum _variable)
        {
            var result = _variable.GetType().GetMember(_variable.ToString()).First().GetCustomAttributes<DisplayAttribute>().First().Name; 
            return result;
        }

        //Para birimi eklemek icin extension olusturuldu
        public static string AddToCurrency(this int item)
        {
            return  item  + " TRY";
        }
    }
}
