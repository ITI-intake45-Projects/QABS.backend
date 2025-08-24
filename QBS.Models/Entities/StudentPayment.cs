
namespace QABS.Models
{
    public class StudentPayment
    {

        public int Id { get; set; }

        public decimal Amount { get; set; }     // المبلغ المدفوع

        public StudentPaymentStatus Status { get; set; } 
        public DateTime PaymentDate { get; set; } 

        //public string? PaymentMethod { get; set; }  // Cash, Card, etc.

        public string ? ImageUrl {  get; set; } // صورة من ايصال الدفع

        // العلاقة مع Student
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }

    }
}
