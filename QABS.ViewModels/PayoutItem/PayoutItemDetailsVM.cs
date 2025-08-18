

namespace QABS.ViewModels
{
    public class PayoutItemDetailsVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int TeacherPayoutId { get; set; }
        public SessionDetailsVM SessionDetails { get; set; }

    }
}
