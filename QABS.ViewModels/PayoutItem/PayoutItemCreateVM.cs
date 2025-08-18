

namespace QABS.ViewModels
{
    public class PayoutItemCreateVM
    {
        public decimal Amount { get; set; } // price for teacher Amount = hourlyrate * 40 /60(session duration) 
        public int TeacherPayoutId { get; set; }
        public int SessionId { get; set; }
    }
}
