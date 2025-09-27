

using QABS.Models;

namespace QABS.ViewModels
{
    public class TeacherListMoreInfoVM
    {
        public string TeacherId { get; set; }
        public string FullName { get; set; }
        public string? ProfileImg { get; set; }
        public int? EnrollmentsCount { get; set; }
        public List<SpecializationType> Specializations { get; set; }

    }
}
