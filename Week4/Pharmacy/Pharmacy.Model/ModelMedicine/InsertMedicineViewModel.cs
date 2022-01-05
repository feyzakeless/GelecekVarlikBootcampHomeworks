using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model.ModelMedicine
{
    //İlac ekleme modeli olusturuldu
    public class InsertMedicineViewModel
    {
        [Required(ErrorMessage = "Ürün adı bilgisi zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ürün etiket alanı zorunludur.")]
        [StringLength(8, ErrorMessage = "Ürün etkiket kodu 8 karakter olmamalıdır.")]
        public string TicketCode { get; set; }

        [Required(ErrorMessage = "Ürün açıklama bilgisi zorunludur.")]
        [StringLength(500, ErrorMessage = "Ürün açıklaması 500 karakterden fazla olmamalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ürün fiyat bilgisi zorunludur.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Ürün stok bilgisi zorunludur.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int Iuser { get; set; }
    }
}
