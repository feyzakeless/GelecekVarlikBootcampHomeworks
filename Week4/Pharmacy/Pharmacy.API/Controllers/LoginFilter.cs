using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Pharmacy.API.Infrastructure;
using Pharmacy.Extension;
using Pharmacy.Model.ModelLogin;
using Pharmacy.Model.ModelUser;
using System;

namespace Pharmacy.API.Controllers
{
    //Attribute tanimlama
    public class LoginFilter : Attribute, IActionFilter
    {

        private readonly IMemoryCache memoryCache;
        public LoginFilter(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }

        /// <summary>
        /// UserType1 = Hasta,
        /// UserType2 = Eczacı,
        /// UserType3 = Doktor
        /// </summary>
        string userType = ExtensionFile.GetEnum(Pharmacy.Extension.Enum.UserType3); //Authorized User

        //Attribute u sagladiktan sonraki islem
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (userType == "Hasta") //Kullanici hastaysa ilacları listeleme
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Medicine", Action = "GetMedicines" }));
            //}
            //else if (userType == "Eczacı") //Kullanici eczaciysa receteleri listeleme
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Prescription", Action = "GetPrescription" }));
            //}
            //else if (userType == "Doktor") //Kullanici doktorsa hastalari listeleme
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "User", Action = "GetPatients" }));
            //}
            return;
        }

        //Attribute u saglamadan önceki kosul
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!memoryCache.TryGetValue(CacheKeys.Login, out UserViewModel response))
            {
                context.Result = new BadRequestObjectResult(error: "Kullanici bulunamadı.");  
            }
        }
    }
}
