

namespace QABS.ViewModels
{
    public class TeacherAvailabilityDetailsVM
    {
        public int Id { get; set; }
        
        // اليوم (ممكن Enum أو int يمثل يوم الأسبوع)
        public DayOfWeek DayOfWeek { get; set; }
        
        // وقت البداية والنهاية
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        
        // علاقة بالمدرس
        public string TeacherId { get; set; }
        
      
    }
}
