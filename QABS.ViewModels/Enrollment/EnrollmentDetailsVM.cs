

using QABS.Models;

namespace QABS.ViewModels
{
    public class EnrollmentDetailsVM
    {
        public int Id { get; set; }

        public string? StudentName { get; set; }
        public string? TeacherName { get; set; }
        public string? SpecializationName { get; set; }

        public SubscriptionType SubscriptionType { get; set; }
        public EnrollmentStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal AgreedHourlyRate { get; set; }

        public int RemainingSessions { get; set; }

        // معلومات إضافية
        public List<string> CompletedLevels { get; set; } = new List<string>();
        public int TotalSessions => Sessions.Count();
        public IEnumerable<SessionVM> Sessions { get; set; } = new List<SessionVM>();

    }
}
