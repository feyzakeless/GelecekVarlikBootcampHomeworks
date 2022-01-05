using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.UserServiceLayer;

namespace Pharmacy.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IDistributedCache distributedCache;

        public UserController(IUserService _userService, IDistributedCache _distributedCache)
        {
            userService = _userService;
            distributedCache = _distributedCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Kullanici ekleme(get)
        [HttpGet]
        public IActionResult InsertUser()
        {
           return View();
            
        }

        //Kullanici ekleme(post)
        [HttpPost]
        public IActionResult InsertUser(UserViewModel newUser)
        {
            var model = userService.Insert(newUser);

            if (!model.IsSuccess)
            {
                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        //Kullanici listeleme
        public IActionResult ListUser()
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);
            var response = new UserViewModel();

            if (cachedData is not null)
            {
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
                ViewBag.Cache = response.AuthorizeId;
                ViewBag.Name = response.Name + " " + response.Surname;
            }

            return View(userService.GetUsers().List);
        }

        //Kullanici guncelleme(get)
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var model  = userService.GetById(id);    
            return View(model.Entity);
        }

        //Kullanici guncelleme(post)
        [HttpPost]
        public IActionResult UpdateUser(UserViewModel user)
        {
            var model = userService.Update(user.Id, user);
            return RedirectToAction("UpdateUser", "User");
        }
    }
}
