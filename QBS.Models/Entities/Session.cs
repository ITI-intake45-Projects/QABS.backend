
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public SessionStatus Status { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = default!;
        public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();

    }
}
