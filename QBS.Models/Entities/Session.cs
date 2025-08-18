
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public decimal Amount { get; set; }
        public SessionStatus Status { get; set; }
        public int EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; } = default!;
        //public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();

        public int? TeacherPayoutId { get; set; }
        public virtual TeacherPayout? TeacherPayout { get; set;} 

    }
}
