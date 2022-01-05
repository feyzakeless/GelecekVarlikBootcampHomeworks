using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pharmacy.API.Infrastructure;
using Pharmacy.DB.Entities.DataContext;
using Pharmacy.Model.ModelUser;
using System;
using System.Linq;

namespace Pharmacy.API.Controllers
{
    public class AuthorizationFilter : Attribute, IActionFilter
    {
        private readonly IDistributedCache distributedCache;
        public AuthorizationFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);
            var response = new UserViewModel();

            if (!string.IsNullOrEmpty(cachedData))
            {
                //Cachedeki bilgiyi aldım
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
                
                using (var item = new PharmacyContext())
                {
                    //UserType tablosundaki id ile cachedeki AuthorizeId eşleşiyor mu diye kontrol ettim
                    var userCheck = item.UserType.Any(a => a.Id == response.AuthorizeId);

                    if (userCheck)
                    {
                        //Yetki durumuna göre işlemler yaptırdım.
                        if (response.AuthorizeId != 2) //Sistem giren kişi eczacı ise
                        {
                            context.Result = new UnauthorizedObjectResult("Bu işlem için yetkiniz yoktur!");
                        }
                    }

                }

                
            }
            
        }
    }
}
