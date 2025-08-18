
namespace QABS.Models
{
    public class PayoutItem
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }// price for teacher Amount = hourlyrate * 40 /60(session duration) 
        public int TeacherPayoutId { get; set; }
        public TeacherPayout TeacherPayout { get; set; } = default!;
        public int SessionId { get; set; }
        public Session Session { get; set; } = default!;
    }
}
