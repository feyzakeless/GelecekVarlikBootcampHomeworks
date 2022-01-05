using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model.ModelPrescription
{
    // Recete modeli olusturuldu
    public class PrescriptionViewModel
    {
        public string Name { get; set; }
        public int PrescriptionNo { get; set; }
        public bool IsActive { get; set; }
        public int Iuser { get; set; }
        public int Imedicine { get; set; }
    }
}
