
namespace QABS.ViewModels
{
    public class StudentPaymentDetailsVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }     // المبلغ المدفوع
        public DateTime PaymentDate { get; set; }
        public string? ImageUrl { get; set; } // صورة من ايصال الدفع

        public string StudentName { get; set; } // اسم الطالب

        public EnrollmentDetailsVM EnrollmentDetailsVM { get; set; } // تفاصيل التسجيل المرتبط بالدفع
    }
}
