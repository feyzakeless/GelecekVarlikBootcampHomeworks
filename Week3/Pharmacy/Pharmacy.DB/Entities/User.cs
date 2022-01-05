using System;
using System.Collections.Generic;

#nullable disable

namespace Pharmacy.DB.Entities
{
    public partial class User
    {
        public User()
        {
            Medicine = new HashSet<Medicine>();
            Prescription = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public int AuthorizeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Idatetime { get; set; }
        public DateTime? Udatetime { get; set; }
        public int Iuser { get; set; }
        public int? Uuser { get; set; }

        public virtual Enum Authorize { get; set; }
        public virtual ICollection<Medicine> Medicine { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
