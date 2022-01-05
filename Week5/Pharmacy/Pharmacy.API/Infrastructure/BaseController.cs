using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Pharmacy.Model.ModelUser;

namespace Pharmacy.API.Infrastructure
{
    public class BaseController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;
        public BaseController(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        //Geçerli kullanici bilgisine buradan ulasacagiz.
        public UserViewModel CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }
        private UserViewModel GetCurrentUser()
        {
            var response = new UserViewModel();
            //memoryCache.TryGetValue(CacheKeys.Login, out response);

            var cachedData = distributedCache.GetString(CacheKeys.Login);
            if (!string.IsNullOrEmpty(cachedData))
            {
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
            }

            return response;
        }
    }
}
