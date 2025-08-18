
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
                EnrollmentId = vm.EnrollmentId
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
