
using QABS.Models;

namespace QABS.ViewModels
{
    public class UserRegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string? ProfileImg { get; set; }

        //public bool IsDeleted { get; set; } = false;

        //public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Role { get; set; }

        public string Password { get; set; }


    }
}
