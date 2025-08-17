
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class TeacherPayout
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = default!;
        public DateTime PaidAt { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();

    }
}
