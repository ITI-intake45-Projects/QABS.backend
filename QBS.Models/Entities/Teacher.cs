
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class Teacher
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }


        public decimal? HourlyRate { get; set; }

        // تخصص المدرس
        public List<SpecializationType> Specializations { get; set; }
        //public string? Bio { get; set; } // نبذة 


        // اشتراكات الطلاب مع المدرس
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<TeacherPayout> TeachersPayouts { get; set; } = new List<TeacherPayout>();
        // مواعيد التفرغ
        public virtual ICollection<TeacherAvailability> TeacherAvailabilities { get; set; } = new List<TeacherAvailability>();
    }
}

