
using QABS.Models;

namespace QABS.ViewModels
{
    public class StudentListMoreInfoVM
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public Gender? Gender { get; set; }
        public int Age { get; set; }
        public string? ProfileImg { get; set; }
    }
}
