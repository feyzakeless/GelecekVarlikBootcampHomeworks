using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model;
using Pharmacy.Model.ModelLogin;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.UserServiceLayer;
using System;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IDistributedCache distributedCache; //DistributedCache çağırıldı
        private readonly IUserService userService;
        public LoginController(IDistributedCache _distributedCache, IUserService _userService)
        {
            distributedCache = _distributedCache;
            userService = _userService;
        }

        
        [HttpPost]
        //UI tarafında çalışan ben sana email ve password göndericem,sen bana true/false dön diyor.
        public General<bool> Login([FromBody] LoginViewModel loginUser)
           
        {
            General<bool> response = new() { Entity = false };
            General<UserViewModel> _response = userService.Login(loginUser);
            if (_response.IsSuccess)
            {
                /*TryGetValue; git cache e bak,cache de user keyine ait bir veri var mı? Ama bu veri UserViewModel'e karşılık 
                 gelmesi lazım.Eğer varsa out şeklinde dışarıya çıkart, yoksa devam et.*/
                /*if(!memoryCache.TryGetValue(CacheKeys.Login, out UserViewModel _loginUser)) //varsa devam eder,yoksa false dönecek
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(value: 1), //cache sistemde 1 saat tutulacak, sonra silinecek
                        Priority = CacheItemPriority.Normal
                    };
                    memoryCache.Set(CacheKeys.Login, _response.Entity);
                }*/
                var cachedData = distributedCache.GetString(CacheKeys.Login);

                var cacheOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10)
                };

                if (string.IsNullOrEmpty(cachedData))
                {
                    distributedCache.SetString(CacheKeys.Login, JsonConvert.SerializeObject(_response.Entity), cacheOptions);
                }


                response.Entity = true;
                response.IsSuccess = true;

            }

            return response;
        }

    }
}
