

using QABS.Models;

namespace QABS.ViewModels
{
    public class SessionEnrollmentDetailsVM
    {
        public int SessionId { get; set; }
        public DateTime? StartTime { get; set; }
        public SessionStatus? Status { get; set; }

        public int EnrollmentId { get; set; }

        public string? TeacherName { get; set; }
        public string? StudentName { get; set; }

        public string? TeacherImg { get; set; }
        public string? StudentImg { get; set; }

    }
}
