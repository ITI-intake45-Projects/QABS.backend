using QABS.Models;

namespace QABS.ViewModels
{
    public class SessionDetailsVM
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public SessionStatus? Status { get; set; }

        public decimal? Amount { get; set; }
        public int EnrollmentId { get; set; }

        public decimal? Duration { get; set; }

        //list of payout items
        //public List<PayoutItemDetailsVM> PayoutItemsDetails { get; set; } = new List<PayoutItemDetailsVM>();

    }
}
