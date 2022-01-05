namespace Pharmacy.Model.ModelMedicine
{
    //İlac guncelleme modeli olusturuldu
    public class UptadeMedicineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TicketCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Iuser { get; set; }
    }
}
