

namespace QABS.Models
{
    public class Admin 
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }

    
}
