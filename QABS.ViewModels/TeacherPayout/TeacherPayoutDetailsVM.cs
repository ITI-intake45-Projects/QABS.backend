

namespace QABS.ViewModels
{
    public class TeacherPayoutDetailsVM
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; }
        public decimal TotalHours { get; set; }   // optional but useful
        public decimal TotalAmount { get; set; }  // HourlyRate * TotalHours
        public decimal? HourlyRate { get; set; }
        public string? TeacherImage { get; set; }
        public string? ImageUrl { get; set; }
        public List<SessionDetailsVM>? Sessions { get; set; }
    }
}
