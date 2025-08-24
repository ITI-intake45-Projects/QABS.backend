

using QABS.Models;

namespace QABS.ViewModels
{
    public class SubscribtionPlanDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SubscriptionType Type { get; set; }

        public SessionDurationType Duration { get; set; }

        //public decimal MonthlyFee { get; set; }
    }
}
