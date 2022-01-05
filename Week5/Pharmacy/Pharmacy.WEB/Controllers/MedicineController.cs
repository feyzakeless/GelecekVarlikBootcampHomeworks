using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pharmacy.API.Controllers;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model.ModelMedicine;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.MedicineServiceLayer;

namespace Pharmacy.WEB.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService medicineService;
        private readonly IDistributedCache distributedCache;

        public MedicineController( IMedicineService _medicineService, IDistributedCache _distributedCache)
        {
            medicineService = _medicineService;
            distributedCache = _distributedCache;

        }
        public IActionResult Index()
        {
            return View();

        }

        //İlac listeleme
        public IActionResult ListMedicine()
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);
            var response = new UserViewModel();

            if (cachedData is not null)
            {
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
                ViewBag.Cache = response.AuthorizeId;
                ViewBag.Name = response.Name +" "+ response.Surname;
            }

            var model = medicineService.GetList().List;
            return View(model);
        }

        //İlac ekleme(get)
        public IActionResult InsertMedicine()
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);
            var response = new UserViewModel();

            if (cachedData is not null)
            {
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
                ViewBag.Name = response.Name + " " + response.Surname;
            }
            return View();
        }

        //İlac ekleme(post)
        [HttpPost]
        public IActionResult InsertMedicine(InsertMedicineViewModel newMedicine)
        {
            var model = medicineService.Insert(newMedicine);

            if (!model.IsSuccess)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        //İlac guncelleme(get)
        [HttpGet]
        public IActionResult UpdateMedicine(int id)
        {
            var cachedData = distributedCache.GetString(CacheKeys.Login);
            var response = new UserViewModel();

            if (cachedData is not null)
            {
                response = JsonConvert.DeserializeObject<UserViewModel>(cachedData);
                ViewBag.Name = response.Name + " " + response.Surname;
            }

            var model = medicineService.GetById(id);
            return View(model.Entity);
        }

        //İlac guncelleme(post)
        [HttpPost]
         public IActionResult UpdateMedicine(UptadeMedicineViewModel medicine, int id)
         {
            var model = medicineService.Update(medicine , id);

             if (!model.IsSuccess)
             {
                 return View();
             }

             return RedirectToAction("Index", "Home");
         }

        //İlac silme
        public IActionResult DeleteMedicine(int id)
         {
            medicineService.Delete(id);
            return RedirectToAction("Index", "Home");
         }
         
    }
}
