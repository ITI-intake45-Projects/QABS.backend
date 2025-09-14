


using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; } = default!;

        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = default!;

        public int SubscriptionPlanId { get; set; }
        public virtual SubscribtionPlan SubscriptionPlan { get; set; }

        //public SessionDurationType Duration { get; set; }
        public decimal EnrollmentFee { get; set; } // السعر الشهري
        public decimal? Discount { get; set; } // الخصم على الاشتراك


        public SpecializationType Specialization { get; set; } 
        public EnrollmentStatus Status { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public virtual StudentPayment StudentPayment { get; set; }

    }
}
