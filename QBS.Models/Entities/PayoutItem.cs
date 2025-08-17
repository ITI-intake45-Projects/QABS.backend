
namespace QABS.Models
{
    public class PayoutItem
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }// price for teacher
        public int TeacherPayoutId { get; set; }
        public TeacherPayout TeacherPayout { get; set; } = default!;
        public int SessionId { get; set; }
        public Session Session { get; set; } = default!;
    }
}
