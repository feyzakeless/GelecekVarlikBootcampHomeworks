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
        public General<MedicineViewModel> Insert(MedicineViewModel newmedicine);
        public General<MedicineViewModel> GetMedicines();
        public General<MedicineViewModel> Update(int id, MedicineViewModel medicine);
        public General<MedicineViewModel> Delete(int id);

    }
}
