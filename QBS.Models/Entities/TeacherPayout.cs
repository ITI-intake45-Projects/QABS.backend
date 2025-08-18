
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class TeacherPayout
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = default!;
        public DateTime PaidAt { get; set; }
        public decimal TotalHours { get; set; }   // optional but useful
        public decimal TotalAmount { get; set; }  // HourlyRate * TotalHours
        public string? ImageUrl { get; set; }
        //public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();
        public virtual ICollection<Session> sessions { get; set; }

    }
}
