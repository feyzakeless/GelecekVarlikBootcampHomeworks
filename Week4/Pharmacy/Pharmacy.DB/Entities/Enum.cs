using System;
using System.Collections.Generic;

#nullable disable

namespace Pharmacy.DB.Entities
{
    public partial class Enum
    {
        public Enum()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string AuthorizeType { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
