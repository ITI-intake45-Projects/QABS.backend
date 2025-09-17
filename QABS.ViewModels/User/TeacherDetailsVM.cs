

using QABS.Models;

namespace QABS.ViewModels
{
    public class TeacherDetailsVM
    {
        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public int Age { get; set; }

        public string? phoneNumber { get; set; }
        public string? ProfileImg { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? EnrollmentsCount { get; set; }
        public List<SpecializationType> Specializations { get; set; }
        public List<TeacherAvailabilityDetailsVM>? Availability { get; set; }
        public List<TeacherPayoutDetailsVM>? Payouts { get; set; }

    }
}
