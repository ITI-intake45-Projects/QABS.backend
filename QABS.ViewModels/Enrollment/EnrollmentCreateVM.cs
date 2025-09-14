
using QABS.Models;
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class EnrollmentCreateVM
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string TeacherId { get; set; }
        [Required]
        public int SubscriptionPlanId { get; set; }

        [Required]
        public SpecializationType Specialization { get; set; }

        [Required]
        public decimal EnrollmentFee { get; set; } // السعر الشهري
        public decimal? Discount { get; set; } // الخصم على الاشتراك

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public StudentPaymentCreateVM? studentPayment { get; set; }

        [Required]
        public List<DayOfWeek> DaysOfWeek { get; set; } = new();

        [Required]
        public TimeSpan StartTime { get; set; }

    }
}
