using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
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

        private readonly IDistributedCache distributedCache; //DistributedCache çağırıldı
        public LoginFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        //string userType = ExtensionFile.GetEnum(Pharmacy.Extension.Enum.UserType3); //Authorized User

        //Attribute u sagladiktan sonraki islem
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        //Attribute u saglamadan önceki kosul
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);

            if (string.IsNullOrEmpty(cachedData))
            {
                context.Result = new UnauthorizedObjectResult("Lütfen giriş yapınız");
            }
        }
    }
}
