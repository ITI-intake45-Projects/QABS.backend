

using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class TeacherPayoutCreateVM
    {
        public string TeacherId { get; set; }

        [Required]
        public DateTime PaidAt { get; set; }
        public string? ImageUrl { get; set; }
        public List<int>? SessionIds { get; set; } 

    }
}
