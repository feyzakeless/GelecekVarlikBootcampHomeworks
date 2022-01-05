using AutoMapper;
using Pharmacy.DB.Entities.DataContext;
using Pharmacy.Model;
using Pharmacy.Model.ModelPrescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Service.PrescriptionServiceLayer
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IMapper mapper;

        public PrescriptionService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //Recete Ekleme
        public General<PrescriptionViewModel> Insert(PrescriptionViewModel newPrescription)
        {
            var result = new General<PrescriptionViewModel>() { IsSuccess = false };

            var prescriptionModel = mapper.Map<Pharmacy.DB.Entities.Prescription>(newPrescription);
            using (var srv = new PharmacyContext())
            {
                prescriptionModel.Idate = DateTime.Now;
                srv.Prescription.Add(prescriptionModel);
                srv.SaveChanges();
                result.Entity = mapper.Map<PrescriptionViewModel>(prescriptionModel);
                result.IsSuccess = true;
            }



            return result;
        }

        //Recete Guncelleme
        public General<PrescriptionViewModel> Update(int id, PrescriptionViewModel prescription)
        {
            var result = new General<PrescriptionViewModel>();

            using (var context = new PharmacyContext())
            {
                var updatePrescription = context.Prescription.SingleOrDefault(i => i.Id == id);

                if (updatePrescription is not null)
                {
                    updatePrescription.Name = prescription.Name;
                    updatePrescription.PrescriptionNo = prescription.PrescriptionNo;
                    updatePrescription.IsActive = prescription.IsActive;


                    context.SaveChanges();

                    result.Entity = mapper.Map<PrescriptionViewModel>(updatePrescription);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Guncellenecek recete bulunamadı.";
                }
            }

            return result;
        }

        //Recete Silme
        public General<PrescriptionViewModel> Delete(int id)
        {
            var result = new General<PrescriptionViewModel>();

            using (var context = new PharmacyContext())
            {
                var prescription = context.Prescription.SingleOrDefault(i => i.Id == id);

                if (prescription is not null)
                {
                    context.Prescription.Remove(prescription);
                    context.SaveChanges();

                    result.Entity = mapper.Map<PrescriptionViewModel>(prescription);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Silinecek recete bulunamadı.";
                    result.IsSuccess = false;
                }
            }

            return result;
        }

        //Recete Listeleme
        public General<PrescriptionViewModel> GetPrescription()
        {
            var result = new General<PrescriptionViewModel>();

            using (var context = new PharmacyContext())
            {
                var data = context.Prescription
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<PrescriptionViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir recete bulunamadı.";
                }
            }

            return result;
        }

        
    }
}
