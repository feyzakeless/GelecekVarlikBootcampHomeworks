using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Extension
{
    public enum Enum
    {
        
        [Display(Name = "Hasta")]
        UserType1 = 1,
        [Display(Name = "Eczacı")]
        UserType2 = 2,
        [Display(Name = "Doktor")]
        UserType3 = 3
    }
}
