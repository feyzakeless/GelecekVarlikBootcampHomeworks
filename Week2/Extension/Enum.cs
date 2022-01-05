using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public enum Enum
    {
        //Create Enum
        [Display(GroupName = "Yetkili", Name = "Müdür")]
        UserType1 = 1,
        [Display(GroupName = "Yetkili", Name = "Öğretmen")]
        UserType2 = 2,
        [Display(GroupName = "Yetkisiz", Name = "Öğrenci")]
        UserType3 = 3
    }
}
