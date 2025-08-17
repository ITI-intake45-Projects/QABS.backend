
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public SessionStatus Status { get; set; }

        public SessionDurationType Duration { get; set; }

        ///// <summary>السعر الفعلي للجلسة وقت إنشائها من اجل المعلم</summary>
        //public decimal SessionPrice { get; set; }




        //Relations :


        //public int StudentId { get; set; }
        //public virtual Student Student { get; set; } = default!;

        //public int TeacherId { get; set; }
        //public virtual Teacher Teacher { get; set; } = default!;

        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = default!;

        public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();

    }
}
