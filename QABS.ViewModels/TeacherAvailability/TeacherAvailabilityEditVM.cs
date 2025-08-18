
namespace QABS.ViewModels
{
    public class TeacherAvailabilityEditVM
    {
        public DayOfWeek? DayOfWeek { get; set; }

        // وقت البداية والنهاية
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
