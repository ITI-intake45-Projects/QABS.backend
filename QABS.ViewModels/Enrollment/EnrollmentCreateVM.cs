
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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public StudentPaymentCreateVM? studentPayment { get; set; }

    }
}
