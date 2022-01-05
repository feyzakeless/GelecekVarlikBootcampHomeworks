using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model.ModelMedicine
{
    //İlac modeli olusturuldu
    public class MedicineViewModel
    {
        public string Name { get; set; }
        public string TicketCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Iuser { get; set; }
    }
}
