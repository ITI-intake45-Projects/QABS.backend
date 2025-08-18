

namespace QABS.ViewModels
{
    public class TeacherPayoutCreateVM
    {
        public int TeacherId { get; set; }
        public DateTime PaidAt { get; set; }
        public string? ImageUrl { get; set; }
        public List<int> SessionIds { get; set; } 

    }
}
