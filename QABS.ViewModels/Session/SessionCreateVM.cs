
using QABS.Models;
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class SessionCreateVM
    {
        [Required]
        public DateTime StartTime { get; set; }
        public SessionStatus Status { get; set; } = SessionStatus.Scheduled;

        public decimal Amount { get; set; }
      
        [Required]
        public int EnrollmentId { get; set; }

    }
}
