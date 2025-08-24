

namespace QABS.Models
{
    public class Admin 
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
    }

    
}
