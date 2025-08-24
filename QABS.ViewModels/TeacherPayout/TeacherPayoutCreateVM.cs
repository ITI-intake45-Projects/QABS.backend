

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class TeacherPayoutCreateVM
    {
        public string TeacherId { get; set; }

        [Required]
        public DateTime PaidAt { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? TotalHours { get; set; }   // optional but useful
        public decimal? TotalAmount { get; set; }  // HourlyRate * TotalHours
        public IFormFile? ImageFile { get; set; } // صورة من ايصال الدفع (ملف)
        public List<int>? SessionIds { get; set; }

    }
}
