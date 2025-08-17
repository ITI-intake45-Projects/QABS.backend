
namespace QABS.Models
{
    public class Student
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<StudentPayment> StudentPayments { get; set; } = new List<StudentPayment>();



    }
}
