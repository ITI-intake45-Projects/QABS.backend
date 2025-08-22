
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class StudentPaymentCreateVM
    {
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; } // المبلغ المدفوع

        [Required(ErrorMessage = "Payment date is required")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Url(ErrorMessage = "Invalid image URL format")]
        public string? ImageUrl { get; set; } // صورة من ايصال الدفع

        [Required(ErrorMessage = "StudentId is required")]
        public string StudentId { get; set; } // معرف الطالب


        public int? EnrollmentId { get; set; } // معرف التسجيل
    }
}
