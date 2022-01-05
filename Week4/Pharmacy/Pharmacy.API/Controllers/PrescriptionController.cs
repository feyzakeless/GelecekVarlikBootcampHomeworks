using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Model.ModelPrescription;
using Pharmacy.Service.PrescriptionServiceLayer;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService PrescriptionService;
        private readonly IMapper mapper;

        public PrescriptionController(IPrescriptionService _PrescriptionService, IMapper _mapper)
        {
            PrescriptionService = _PrescriptionService;
            mapper = _mapper;
        }

        //Recete Ekleme
        [HttpPost]
        public General<PrescriptionViewModel> Insert([FromBody] PrescriptionViewModel newPrescription)
        {
            var result = false;
            return PrescriptionService.Insert(newPrescription);
        }

        //Recete Listeleme
        [HttpGet]
        public General<PrescriptionViewModel> GetPrescription()
        {
            return PrescriptionService.GetPrescription();
        }

        //Id ye gore recete guncelleme
        [HttpPut("{id}")]
        public General<PrescriptionViewModel> Update(int id, PrescriptionViewModel Prescription)
        {
            return PrescriptionService.Update(id, Prescription);
        }


        //Recete silme
        [HttpDelete]
        public General<PrescriptionViewModel> Delete(int id)
        {
            return PrescriptionService.Delete(id);
        }

    }
}
