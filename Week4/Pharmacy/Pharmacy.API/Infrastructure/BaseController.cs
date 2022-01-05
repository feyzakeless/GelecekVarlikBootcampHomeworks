using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pharmacy.Model.ModelUser;

namespace Pharmacy.API.Infrastructure
{
    public class BaseController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        public BaseController(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
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
            memoryCache.TryGetValue(CacheKeys.Login, out response);

            return response;
        }
    }
}
