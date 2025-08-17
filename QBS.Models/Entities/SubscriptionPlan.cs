

namespace QABS.Models
{
    public class SubscriptionPlan
    {
      
        public int Id { get; set; }

        public SubscriptionType Type { get; set; }

        

        /// <summary>السعر الشهري</summary>
        public decimal MonthlyFee { get; set; }

            // علاقة مع Enrollment
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
       

    }
}
