using AutoMapper;
using Pharmacy.Model.ModelLogin;
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

            //Login Mapping
            CreateMap<LoginViewModel, Pharmacy.DB.Entities.User>();
            CreateMap<Pharmacy.DB.Entities.User, LoginViewModel>();

            //İlac Mapping

            CreateMap<MedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, MedicineViewModel>();

            CreateMap<InsertMedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, InsertMedicineViewModel>();

            CreateMap<ListMedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, ListMedicineViewModel>();

            CreateMap<UptadeMedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, UptadeMedicineViewModel>();

            CreateMap<DeleteMedicineViewModel, Pharmacy.DB.Entities.Medicine>();
            CreateMap<Pharmacy.DB.Entities.Medicine, DeleteMedicineViewModel>();

            //Recete Mapping
            CreateMap<PrescriptionViewModel, Pharmacy.DB.Entities.Prescription>();
            CreateMap<Pharmacy.DB.Entities.Prescription, PrescriptionViewModel>();

        }
    }
}
