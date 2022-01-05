using Pharmacy.Model;
using Pharmacy.Model.ModelMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Service.MedicineServiceLayer
{
    //İlac Interface i olusturuldu.
    public interface IMedicineService
    {
        public General<InsertMedicineViewModel> Insert(InsertMedicineViewModel newMedicine);
        public General<ListMedicineViewModel> GetList();
        public General<UptadeMedicineViewModel> Update(UptadeMedicineViewModel medicine, int id);
        public General<UptadeMedicineViewModel> GetById(int id);
        public General<DeleteMedicineViewModel> Delete(int id);
        public General<ListMedicineViewModel> Pagination(int productNumOnPage, int whichPageNumber);
        public General<ListMedicineViewModel> SortMedicines(string parameter);
        public General<ListMedicineViewModel> FilterMedicine(string filterLetter);

        /*--------------- Week 3 -----------------------------*/
        public General<MedicineViewModel> GetMedicines();
    }
}
