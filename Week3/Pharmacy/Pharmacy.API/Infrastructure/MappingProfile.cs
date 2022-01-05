using AutoMapper;
using Pharmacy.Model.ModelMedicine;
using Pharmacy.Model.ModelPrescription;
using Pharmacy.Model.ModelUser;

namespace Pharmacy.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Kullanici Mapping
            CreateMap<UserViewModel, Pharmacy.DB.Entities.User>();
            CreateMap<Pharmacy.DB.Entities.User, UserViewModel>();

            //İlac Mapping
            CreateMap<MedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, MedicineViewModel>();

            //Recete Mapping
            CreateMap<PrescriptionViewModel, Pharmacy.DB.Entities.Prescription>();
            CreateMap<Pharmacy.DB.Entities.Prescription, PrescriptionViewModel>();

        }
    }
}
