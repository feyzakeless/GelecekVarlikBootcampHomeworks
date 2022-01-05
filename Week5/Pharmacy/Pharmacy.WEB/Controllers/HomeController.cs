using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model;
using Pharmacy.Model.ModelLogin;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.MedicineServiceLayer;
using Pharmacy.Service.UserServiceLayer;
using Pharmacy.WEB.Models;
using System;
using System.Diagnostics;

namespace Pharmacy.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IMedicineService medicineService;
        private readonly IDistributedCache distributedCache;

        public HomeController(ILogger<HomeController> logger, IUserService _userService, IMedicineService _medicineService, IDistributedCache _distributedCache)
        {
            _logger = logger;
            userService = _userService;
            medicineService = _medicineService;
            distributedCache = _distributedCache;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        //Login get islemi
        public IActionResult Login()
        {
            return View(); 
        }

        //Login post islemi
        [HttpPost]
        public IActionResult Login(LoginViewModel loginUser)
        {
            /*var model = userService.Login(loginUser);

            if (!model.IsSuccess)
            {
                return View();
            }*/

            General<UserViewModel> _response = userService.Login(loginUser);
            if (_response.IsSuccess)
            {
                var cachedData = distributedCache.GetString(CacheKeys.Login);
                var cacheOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                };
                if (string.IsNullOrEmpty(cachedData))
                {
                    distributedCache.SetString(CacheKeys.Login, JsonConvert.SerializeObject(_response.Entity), cacheOptions);
                }
                

                return RedirectToAction("ListMedicine", "Medicine");
            }
            else
            {
                return View();
            }
                
        }

        public IActionResult RemoveCacheData()
        {
            
            distributedCache.Remove(CacheKeys.Login);
            return RedirectToAction("Login", "Home");
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
