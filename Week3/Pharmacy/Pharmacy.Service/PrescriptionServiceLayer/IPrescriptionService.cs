using Pharmacy.Model;
using Pharmacy.Model.ModelPrescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Service.PrescriptionServiceLayer
{
    //Recete Interface i olusturuldu.
    public interface IPrescriptionService
    {
        public General<PrescriptionViewModel> Insert(PrescriptionViewModel newPrescription);
        public General<PrescriptionViewModel> GetPrescription();
        public General<PrescriptionViewModel> Update(int id, PrescriptionViewModel prescription);
        public General<PrescriptionViewModel> Delete(int id);
    }
}
