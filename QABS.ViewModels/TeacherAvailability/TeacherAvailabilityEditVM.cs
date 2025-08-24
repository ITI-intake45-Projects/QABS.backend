
namespace QABS.ViewModels
{
    public class TeacherAvailabilityEditVM
    {
        public int Id { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }

        // وقت البداية والنهاية
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }


    }
}
