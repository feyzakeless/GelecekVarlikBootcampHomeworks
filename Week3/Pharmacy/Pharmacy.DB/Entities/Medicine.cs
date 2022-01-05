using System;
using System.Collections.Generic;

#nullable disable

namespace Pharmacy.DB.Entities
{
    public partial class Medicine
    {
        public Medicine()
        {
            Prescription = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TicketCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Idate { get; set; }
        public DateTime? Udate { get; set; }
        public int Iuser { get; set; }
        public int? Uuser { get; set; }

        public virtual User IuserNavigation { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
