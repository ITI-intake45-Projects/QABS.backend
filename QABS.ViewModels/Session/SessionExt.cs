
using QABS.Models;

namespace QABS.ViewModels
{
    public static class SessionExt
    {
        public static Session ToCreate(this SessionCreateVM vm)
        {
            return new Session
            {
                StartTime = vm.StartTime,
                EnrollmentId = vm.EnrollmentId,
                Amount = vm.Amount,
                //Amount = session.Enrollment.Teacher.HourlyRate * (decimal)((int)session.Enrollment.SubscriptionPlan.Duration/60.00),


            };
        }
        public static SessionDetailsVM ToDetails(this Session session)
        {
            return new SessionDetailsVM
            {
                Id = session.Id,
                StartTime = session.StartTime,
                Status = session.Status,
                EnrollmentId = session.EnrollmentId,
                Amount = session.Amount,
               
                //PayoutItemsDetails = session.PayoutItems.Select(p => p.ToDetails()).ToList()
            };
        }
        public static Session ToEdit(this SessionEditVM newsession, Session OldSession)
        {

            OldSession.StartTime = newsession.StartTime == default ? OldSession.StartTime : newsession.StartTime;

            return OldSession;
        }
    }
}
