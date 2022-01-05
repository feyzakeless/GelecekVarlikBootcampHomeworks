using AutoMapper;
using Pharmacy.DB.Entities.DataContext;
using Pharmacy.Model;
using Pharmacy.Model.ModelMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Service.MedicineServiceLayer
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper mapper; //Mapper çagrildi

        public MedicineService(IMapper _mapper)
        {
            mapper = _mapper;
        }


        //ilac ekleme islemi
        public General<MedicineViewModel> Insert(MedicineViewModel newmedicine)
        {
            var result = new General<MedicineViewModel>() { IsSuccess = false };

            var medicineModel = mapper.Map<Pharmacy.DB.Entities.Medicine>(newmedicine);
            using (var srv = new PharmacyContext())
            {
                medicineModel.Idate = DateTime.Now;
                srv.Medicine.Add(medicineModel);
                srv.SaveChanges();
                result.Entity = mapper.Map<MedicineViewModel>(medicineModel);
                result.IsSuccess = true;
            }



            return result;
        }

        //ilac guncelleme islemi
        public General<MedicineViewModel> Update(int id, MedicineViewModel medicine)
        {
            var result = new General<MedicineViewModel>();

            using (var context = new PharmacyContext())
            {
                var updateMedicine = context.Medicine.SingleOrDefault(i => i.Id == id);

                if (updateMedicine is not null)
                {
                    updateMedicine.Name = medicine.Name;
                    updateMedicine.TicketCode = medicine.TicketCode;
                    updateMedicine.Description = medicine.Description;
                    updateMedicine.Stock = medicine.Stock;
                    updateMedicine.Price = medicine.Price;

                    context.SaveChanges();

                    result.Entity = mapper.Map<MedicineViewModel>(updateMedicine);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Guncellenecek ilac bulunamadı.";
                }
            }

            return result;
        }

        //ilac silme islemi
        public General<MedicineViewModel> Delete(int id)
        {
            var result = new General<MedicineViewModel>();

            using (var context = new PharmacyContext())
            {
                var medicine = context.Medicine.SingleOrDefault(i => i.Id == id);

                if (medicine is not null)
                {
                    context.Medicine.Remove(medicine);
                    context.SaveChanges();

                    result.Entity = mapper.Map<MedicineViewModel>(medicine);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Silinecek ilac bulunamadı.";
                    result.IsSuccess = false;
                }
            }

            return result;
        }

        //ilac listeleme islemi
        public General<MedicineViewModel> GetMedicines()
        {
            var result = new General<MedicineViewModel>();

            using (var context = new PharmacyContext())
            {
                var data = context.Medicine
                    .Where(x => !x.IsActive && !x.IsDeleted)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<MedicineViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir ilac bulunamadı.";
                }
            }

            return result;
        }

        
    }
}
