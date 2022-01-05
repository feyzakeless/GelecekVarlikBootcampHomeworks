using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Model.ModelMedicine;
using Pharmacy.Service.MedicineServiceLayer;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService medicineService;
        private readonly IMapper mapper;

        public MedicineController(IMedicineService _medicineService, IMapper _mapper)
        {
            medicineService = _medicineService;
            mapper = _mapper;
        }

        //İlac Ekleme
        [HttpPost]
        public General<MedicineViewModel> Insert([FromBody] MedicineViewModel newProduct)
        {
            var result = false;
            return medicineService.Insert(newProduct);
        }
        //İlac id sine gore guncelleme
        [HttpPut("{id}")]
        public General<MedicineViewModel> Update(int id, MedicineViewModel product)
        {
            return medicineService.Update(id, product);
        }

        //İlac silme
        [HttpDelete]
        public General<MedicineViewModel> Delete(int id)
        {
            return medicineService.Delete(id);
        }

        //İlac Listeleme
        [HttpGet]
        public General<MedicineViewModel> GetMedicines()
        {
            return medicineService.GetMedicines();
        }

        

    }
}
