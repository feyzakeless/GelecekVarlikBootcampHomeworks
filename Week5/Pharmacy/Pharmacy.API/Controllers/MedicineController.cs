using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model;
using Pharmacy.Model.ModelMedicine;
using Pharmacy.Service.MedicineServiceLayer;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    /*UMedicineConteroller, bir Base den türetiliyor. Ve bu Base in bir constructor ı var. Ve constructor da memoryCache i istiyor.
     Constructor ı vermezsek kalıtım alamayız. */
    public class MedicineController : BaseController
    {
        private readonly IMedicineService medicineService; //İlac servis cagrildi
        private readonly IMapper mapper;

        public MedicineController(IMedicineService _medicineService, IMapper _mapper, IDistributedCache _distributedCache) : base(_distributedCache)
        {
            medicineService = _medicineService;
            mapper = _mapper;
        }

        //İlac Ekleme
        [HttpPost]
        [ServiceFilter(typeof(LoginFilter))]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public General<InsertMedicineViewModel> Insert([FromBody] InsertMedicineViewModel newMedicine)
        {
            General<InsertMedicineViewModel> response = new();
            response = medicineService.Insert(newMedicine);
            return response;
        }

        
        //İlac Listeleme
        [HttpGet]
        public General<ListMedicineViewModel> GetList()
        {
            return medicineService.GetList();
        }


      
        //Id ye gore Ilac Guncelleme
        [HttpPut("{id}")]
        [ServiceFilter(typeof(LoginFilter))]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public General<UptadeMedicineViewModel> Update([FromBody]  UptadeMedicineViewModel medicine , int id)
        {
            return medicineService.Update(medicine , id);
        }

        
        //İlac silme
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public General<DeleteMedicineViewModel> Delete(int id)
        {
            return medicineService.Delete(id);
        }


        //İlaclari sayfalama
        [HttpGet]
        [Route("Pagination")]
        public General<ListMedicineViewModel> Pagination([FromQuery] int productNumOnPage, [FromQuery] int whichPageNumber)
        {
            return medicineService.Pagination(productNumOnPage, whichPageNumber);            
        }

        // İlaclari Sıralama
        [HttpGet]
        [Route("Sorting")]
        public General<ListMedicineViewModel> SortMedicines([FromQuery] string parameter)
        {
            return medicineService.SortMedicines(parameter);
        }

        // İlaclari Harfe gore Filtreleme
        [HttpGet]
        [Route("Filtering")]
        public General<ListMedicineViewModel> FilterMedicine([FromQuery] string filterLetter)
        {
            return medicineService.FilterMedicine(filterLetter);
        }



        /*---------------------- Week 3 -------------------------*/

        //İlac Listeleme
        //[HttpGet]
        //public General<MedicineViewModel> GetMedicines()
        //{
        //    return medicineService.GetMedicines();
        //}

        

    }
}
