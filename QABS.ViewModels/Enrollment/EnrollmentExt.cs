
using QABS.Models;

namespace QABS.ViewModels
{
    public static class EnrollmentExt
    {
        public static Enrollment ToCreate(this EnrollmentCreateVM enrollment)
        {
            return new Enrollment
            {
                StudentId = enrollment.StudentId,
                TeacherId = enrollment.TeacherId,
                SubscriptionPlanId = enrollment.SubscriptionPlanId,
                Specialization = enrollment.Specialization,
                StartDate = enrollment.StartDate,
                EndDate = enrollment.EndDate,
                StudentPayment = enrollment.studentPayment.Select(c => c.ToCreate()).ToList(),
            };
        }

        public static EnrollmentDetailsVM ToDetails(this Enrollment enrollmentDetails)
        {
            return new EnrollmentDetailsVM
            {
                Id = enrollmentDetails.Id,
                StudentId = enrollmentDetails.StudentId,
                StudentName = enrollmentDetails.Student.User.FirstName +" "+ enrollmentDetails.Student.User.LastName,
                TeacherName = enrollmentDetails.Teacher.User.FirstName +" "+ enrollmentDetails.Teacher.User.LastName,
                TeacherId = enrollmentDetails.TeacherId,
                StudentImg = enrollmentDetails.Student.User.ProfileImg,
                TeacherImg = enrollmentDetails.Teacher.User.ProfileImg,
                SpecializationType = enrollmentDetails.Specialization,
                Status = enrollmentDetails.Status,
                StartDate = enrollmentDetails.StartDate,
                EndDate = enrollmentDetails.EndDate,
                RemainingSessions = enrollmentDetails.Sessions.Count(s => s.Status == SessionStatus.Scheduled),
                StudentPaymentAmount = enrollmentDetails.StudentPayment?.Amount ?? 0,
                SubscriptionPlanDetails = enrollmentDetails.SubscriptionPlan.ToDetails(),
                Sessions = enrollmentDetails.Sessions.Select(s => s.ToDetails()).ToList()
            };
        }
    }
}
