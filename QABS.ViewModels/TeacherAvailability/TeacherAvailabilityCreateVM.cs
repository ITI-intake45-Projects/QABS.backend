

using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class TeacherAvailabilityCreateVM
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        public string TeacherId { get; set; }
        
    }
}
