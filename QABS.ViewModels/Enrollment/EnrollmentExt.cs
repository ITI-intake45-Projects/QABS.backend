
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
                EnrollmentFee = enrollment.EnrollmentFee,
                Discount = enrollment.Discount ?? 0,

                //StudentPayment = enrollment.studentPayment.ToCreate()
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
                EnrollmentFee = enrollmentDetails.EnrollmentFee,
                Discount = enrollmentDetails.Discount ?? 0,
                ActualFee = enrollmentDetails.Discount > 0  ? (100 * enrollmentDetails.EnrollmentFee) / enrollmentDetails.Discount : enrollmentDetails.EnrollmentFee,
                RemainingSessions = enrollmentDetails.Sessions.Count(s => s.Status == SessionStatus.Scheduled),
                StudentPaymentAmount = enrollmentDetails.StudentPayment?.Amount ?? 0,
                SubscriptionPlanDetails = enrollmentDetails.SubscriptionPlan.ToDetails(),
                Sessions = enrollmentDetails.Sessions.Select(s => s.ToDetails()).ToList()
            };
        }
    }
}
