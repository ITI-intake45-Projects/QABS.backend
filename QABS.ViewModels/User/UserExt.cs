
using QABS.Models;

namespace QABS.ViewModels
{
    public static class UserExt
    {
        public static AppUser ToCreate(this UserRegisterVM viewmodel)
        {
            return new AppUser
            {
                FirstName = viewmodel.FirstName,
                LastName = viewmodel.LastName,
                Email = viewmodel.Email,
                Gender = viewmodel.Gender,
                Age = viewmodel.Age,
                ProfileImg = viewmodel.ProfileImg,
                DateCreated = DateTime.UtcNow,

            };
        }

        public static TeacherDetailsVM ToDetails(this Teacher teacher)
        {
            return new TeacherDetailsVM
            {
                TeacherId = teacher.UserId,
                FirstName = teacher.User.FirstName,
                LastName = teacher.User.LastName,
                Gender = teacher.User.Gender,
                Age = teacher.User.Age,
                ProfileImg = teacher.User.ProfileImg,
                DateCreated = teacher.User.DateCreated,
                LastLoginDate = teacher.User.LastLoginDate,
                HourlyRate = teacher.HourlyRate,
                Specializations = teacher.Specializations ?? new List<SpecializationType>(),
                EnrollmentsCount = teacher.Enrollments?.Count,
                Availability = teacher.TeacherAvailabilities?.Select(a => a.ToDetails()).ToList()
            };
        }

        public static TeacherListVM ToList(this Teacher teacher)
        {
            return new TeacherListVM
            {
                TeacherId = teacher.UserId,
                FullName = $"{teacher.User.FirstName} {teacher.User.LastName}",
                ProfileImg = teacher.User.ProfileImg
            };
        }
        public static StudentDetailsVM ToDetails(this Student student)
        {
            return new StudentDetailsVM
            {
                StudentId = student.UserId,
                FirstName = student.User.FirstName,
                LastName = student.User.LastName,
                Gender = student.User.Gender,
                Age = student.User.Age,
                ProfileImg = student.User.ProfileImg,
                DateCreated = student.User.DateCreated,
                LastLoginDate = student.User.LastLoginDate,
                Studentpayments = student.StudentPayments?.Select(sp => sp.ToDetails()).ToList()
                

            };
        }
        public static List<StudentDetailsVM> ToDetails(this List<Student> students)
        {
            return students.Select(s => s.ToDetails()).ToList();
        }
        public static StudentListVM ToList(this Student student)
        {
            return new StudentListVM
            {
                StudentId = student.UserId,
                FullName = $"{student.User.FirstName} {student.User.LastName}",
                ProfileImg = student.User.ProfileImg
            };
        }
    }
}
