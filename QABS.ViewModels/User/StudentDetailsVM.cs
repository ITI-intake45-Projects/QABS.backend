
using QABS.Models;

namespace QABS.ViewModels
{
    public class StudentDetailsVM
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public int Age { get; set; }
        public string? ProfileImg { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public List<StudentPaymentDetailsVM>? Studentpayments { get; set; }

        //public SpecializationType? Specialization { get; set; }

    }
}
