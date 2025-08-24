

using QABS.Models;
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class SubscribtionPlanCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public SubscriptionType Type { get; set; }
        [Required]
        public SessionDurationType Duration { get; set; }
        //[Required]
        //public decimal MonthlyFee { get; set; }
    }
}
