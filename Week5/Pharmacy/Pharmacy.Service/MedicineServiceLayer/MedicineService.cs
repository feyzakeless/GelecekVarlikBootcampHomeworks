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
        public General<InsertMedicineViewModel> Insert(InsertMedicineViewModel newMedicine)
        {
            var result = new General<InsertMedicineViewModel>() { IsSuccess = false };

            try {
                var medicineModel = mapper.Map<Pharmacy.DB.Entities.Medicine>(newMedicine);
                using (var context = new PharmacyContext())
                {
                    var isChecked = context.User.Any(a => a.Id == medicineModel.Iuser); //Kullanici Kontrolu

                    if (isChecked)
                    {
                        medicineModel.Idate = DateTime.Now;
                        context.Medicine.Add(medicineModel);
                        context.SaveChanges();
                    }
                    else
                    {
                        result.ExceptionMessage = "Ekleme işlemi için yetkiniz bulunmamaktadır.";
                    }



                    result.Entity = mapper.Map<InsertMedicineViewModel>(medicineModel);
                    result.IsSuccess = true;
                }
            }
            catch (System.Exception)
            {
                result.ExceptionMessage = "Ürün ekleme gerçekleşmedi.";
            }

            return result;
        }

        //ilac listeleme islemi
        public General<ListMedicineViewModel> GetList()
        {
            var result = new General<ListMedicineViewModel>() { IsSuccess = false };
            using (var context = new PharmacyContext())
            {
                var listedMedicine = context.Medicine.OrderBy(i => i.Id);

                if (listedMedicine.Any())
                {
                    result.List = mapper.Map<List<ListMedicineViewModel>>(listedMedicine);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Ürün bulunamadi.";
                }

            }

            return result;

        }
        

        //ilac guncelleme islemi
        public General<UptadeMedicineViewModel> Update(UptadeMedicineViewModel medicine, int id)
        {

            var result = new General<UptadeMedicineViewModel>() { IsSuccess = false };
            using (var context = new PharmacyContext())
            {
                var updateMedicine = context.Medicine.SingleOrDefault(i => i.Id == id);
                var isChecked = context.User.Any(a => a.Id == medicine.Iuser);

                if (isChecked)
                {
                    if(updateMedicine is not null)
                    {
                        updateMedicine.Name = medicine.Name;
                        updateMedicine.TicketCode = medicine.TicketCode;
                        updateMedicine.Description = medicine.Description;
                        updateMedicine.Price = medicine.Price;
                        updateMedicine.Stock = medicine.Stock;

                        context.SaveChanges();

                        result.Entity = mapper.Map<UptadeMedicineViewModel>(updateMedicine);
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ExceptionMessage = "Ürün bulunamadi.";
                    }
                   
                }
                else
                {
                    result.ExceptionMessage = "Ekleme islemi icin yetkiniz bulunmamaktadir.";
                }
            }

            return result;
        }

        //ilaci id ye gore guncelleme islemi
        public General<UptadeMedicineViewModel> GetById(int id)
        {
            var result = new General<UptadeMedicineViewModel>();

            using (var context = new PharmacyContext())
            {
                // eğer ürün silinmemişse Id'sine göre listeliyoruz
                var data = context.Medicine.
                            SingleOrDefault(x => x.Id == id && !x.IsDeleted);

                // gelen veri varsa işlem başarılı yoksa belirttiğimiz mesaj dönüyor
                if (data is not null)
                {
                    result.Entity = mapper.Map<UptadeMedicineViewModel>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Herhangi bir ilaç bulunamadı.";
                }
            }

            return result;
        }

        //ilac silme islemi
        public General<DeleteMedicineViewModel> Delete(int id)
        {
            var result = new General<DeleteMedicineViewModel>();
            using (var context = new PharmacyContext())
            {
                var deleteMedicine = context.Medicine.SingleOrDefault(i => i.Id == id);

                    if (deleteMedicine is not null)
                    {
                        context.Medicine.Remove(deleteMedicine);
                        context.SaveChanges();

                        result.Entity = mapper.Map<DeleteMedicineViewModel>(deleteMedicine);
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

        //ilac sayfalama islemi
        public General<ListMedicineViewModel> Pagination(int productNumOnPage, int whichPageNumber)
        {
            var result = new General<ListMedicineViewModel>();
            int TotalMedicine = 0;
            decimal totalPageNumber = 0;
            decimal subTotal = 0;

            using (var context = new PharmacyContext())
            {
                //Toplam ilac sayısını alındı
                result.TotalCount = context.Medicine.Count();
                TotalMedicine = result.TotalCount;

                //İlac sayisi, sayfada gösterilecek urun sayisina bolundu ve kaç sayfa olacaği bulundu.
                subTotal = TotalMedicine / productNumOnPage;

                /*Kalan varsa yukari sayiya dondurme. Mesela; 102 urun varsa ve sayfada 20 urun gosterilmesi isteniyorsa,
                kalan 2 oldugu icin 5 sayfa degil, 6 sayfa olması gerekir ve son sayfada 2 urun olmasi gerekir. Math.Ceiling
                5,1 cikan degeri 6 ya tamamliyor.*/
                totalPageNumber =  Math.Ceiling(subTotal);

                /*Skip; O kadar urun atla demek. Mesela; 7 urun var, bir sayfada 2 urun gosterilsin ve 3.sayfa gosterilsin isteniyor.
                 (3-1)*2 = 4. Yani 4 urunu atla 5.urunden basla demek.*/
                //Take; Kac urun alınacağını soylers.
                var medicines = context.Medicine
                                        .OrderBy(i => i.Id)
                                        .Skip((whichPageNumber - 1) * productNumOnPage)
                                        .Take(productNumOnPage).ToList();

                result.List = mapper.Map<List<ListMedicineViewModel>>(medicines);
                result.IsSuccess = true;
            }

            result.SumPageNumber = Decimal.ToInt32(totalPageNumber);

            return result;
        }

        //ilac siralama islemi
        public General<ListMedicineViewModel> SortMedicines(string parameter)
        {
            var result = new General<ListMedicineViewModel>();
            using (var context = new PharmacyContext())
            {
                var medicines = context.Medicine.Where(x => !x.IsDeleted);

                switch (parameter)
                {
                    case "AToZ": // A'dan Z'ye siralama
                        medicines = medicines.OrderBy(x => x.Name);
                        break;
                    case "ZToA": // Z'den A'ya siralama
                        medicines = medicines.OrderByDescending(x => x.Name);
                        break;
                    case "PriceLowToHigh": // Dusuk fiyattan yuksek fiyata siralama
                        medicines = medicines.OrderBy(x => x.Price);
                        break;
                    case "PriceHighToLow": // Yuksek fiyattan dusuk fiyata siralama
                        medicines = medicines.OrderByDescending(x => x.Price);
                        break;
                    case "FirstInsertDate": // İlk eklenen tarihe gore siralama
                        medicines = medicines.OrderBy(x => x.Idate);
                        break;
                    case "LastInsertDate": // Son eklenen tarihe gore siralama
                        medicines = medicines.OrderByDescending(x => x.Idate);
                        break;
                    default:
                        medicines = medicines.OrderBy(x => x.Id); //Default u id ye gore siralama
                        break;
                }

                result.List = mapper.Map<List<ListMedicineViewModel>>(medicines);
            }

            return result;
        }

        //ilaci harfe gore filreleme islemi
        public General<ListMedicineViewModel> FilterMedicine(string filterLetter)
        {
            var result = new General<ListMedicineViewModel>();
            using (var context = new PharmacyContext())
            {
                var medicines = context.Medicine.Where(x => !x.IsDeleted);

                if (!String.IsNullOrEmpty(filterLetter))
                {
                    //İlac isimlerinin hangi harf ile basladigi aliniyor
                    medicines = medicines.Where(x => x.Name.StartsWith(filterLetter)); 
                }
                else
                {
                    result.ExceptionMessage = "Lütfen baska bir harf giriniz.";
                    return result;
                }

                result.List = mapper.Map<List<ListMedicineViewModel>>(medicines);
            }

            return result;
        }

        /*----------------------------- Week 3 ----------------------------------*/

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
