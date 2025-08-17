


using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace QABS.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }

        public int Age { get; set; }

        public string? ProfileImg { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; } 

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }

        public virtual Teacher? Teacher { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Admin? Admin { get; set; }

    }
}
