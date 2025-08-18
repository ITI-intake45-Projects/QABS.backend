
using QABS.Models;
using System.Security.Cryptography;

namespace QABS.ViewModels
{
    public static class TeacherAvailabilityExt
    {

        public static TeacherAvailability ToCreate(this TeacherAvailabilityCreateVM create)
        {
            return new TeacherAvailability
            {
                DayOfWeek = create.DayOfWeek,
                StartTime = create.StartTime,
                EndTime = create.EndTime,
                TeacherId = create.TeacherId
            };
        }

        public static TeacherAvailabilityDetailsVM ToDetails(this TeacherAvailability availability)
        {
            
            return new TeacherAvailabilityDetailsVM
            {
                Id = availability.Id,
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                TeacherId = availability.TeacherId
            };
        }

        public static TeacherAvailability ToEdit(this TeacherAvailabilityEditVM edit , TeacherAvailability old)
        {

            old.DayOfWeek = edit.DayOfWeek ?? old.DayOfWeek;
            old.StartTime = edit.StartTime ?? old.StartTime;
            old.EndTime = edit.EndTime ?? old.EndTime;
            

            return old;
        }
    }
}
