namespace Pharmacy.Model.ModelMedicine
{
    //İlaclari listeleme modeli olusturuldu
    public class ListMedicineViewModel
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
