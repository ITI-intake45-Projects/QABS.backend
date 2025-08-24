

using QABS.Models;

namespace QABS.ViewModels
{
    public static class TeacherPayoutExt
    {
        public static TeacherPayout ToCreate(this TeacherPayoutCreateVM vm)
        {
            return new TeacherPayout
            {
                TeacherId = vm.TeacherId,
                PaidAt = vm.PaidAt,
                ImageUrl = vm.ImageUrl,
                TotalHours = vm.TotalHours ?? 0, // Default to 0 if not provided
                TotalAmount = vm.TotalAmount ?? 0, // Default to 0 if not provided
            };
        }

        public static TeacherPayoutDetailsVM ToDetails(this TeacherPayout model)
        {
            return new TeacherPayoutDetailsVM
            {
                Id = model.Id,
                TeacherId = model.TeacherId,
                TeacherName = model.Teacher.User.FirstName +" "+ model.Teacher.User.LastName ?? string.Empty,
                PaidAt = model.PaidAt,
                HourlyRate = model.Teacher.HourlyRate,
                TeacherImage = model.Teacher.User.ProfileImg,
                ImageUrl = model.ImageUrl,
                TotalHours = model.TotalHours,
                TotalAmount = model.TotalAmount,
                Sessions = model.sessions?.Select(s => s.ToDetails()).ToList()


            };
        }

        public static TeacherPayoutListVM ToList(this TeacherPayout model)
        {
            return new TeacherPayoutListVM
            {
                Id = model.Id,
                TeacherId = model.TeacherId,
                TeacherName = model.Teacher.User.FirstName + " " + model.Teacher.User.LastName ?? string.Empty,
                PaidAt = model.PaidAt,
                TeacherImage = model.Teacher.User.ProfileImg,
                TotalHours = model.TotalHours,
                TotalAmount = model.TotalAmount,
            };
        }
    }
}
