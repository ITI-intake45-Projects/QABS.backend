

using QABS.Models;

namespace QABS.ViewModels
{
    public class EnrollmentDetailsVM
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? StudentImg { get; set; }
        public string? TeacherImg { get; set; }
        public decimal EnrollmentFee { get; set; } // السعر الشهري
        public decimal? Discount { get; set; } // الخصم على الاشتراك
        public decimal? ActualFee { get; set; } // السعر الفعلي بعد الخصم

        public SpecializationType SpecializationType { get; set; }
        public EnrollmentStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal StudentPaymentAmount { get; set; }
        public int RemainingSessions { get; set; }

        // معلومات إضافية
        public int TotalSessions => Sessions.Count();
        public SubscribtionPlanDetailsVM SubscriptionPlanDetails { get; set; }
        public List<SessionDetailsVM>? Sessions { get; set; } 

    }
}
