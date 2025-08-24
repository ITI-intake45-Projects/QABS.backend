

using QABS.Models;

namespace QABS.ViewModels
{
    public static class SubscribtionPlanExt
    {
        public static SubscribtionPlan ToCreate(this SubscribtionPlanCreateVM subscribtionPlanCreateVM)
        {
            return new SubscribtionPlan
            {
                Duration = subscribtionPlanCreateVM.Duration,
                Name = subscribtionPlanCreateVM.Name,
                Type = subscribtionPlanCreateVM.Type,
                //MonthlyFee = subscribtionPlanCreateVM.MonthlyFee,

            };
        }



        public static SubscribtionPlanDetailsVM ToDetails(this SubscribtionPlan model)
        {
            return new SubscribtionPlanDetailsVM
            {
                Id = model.Id,
                Name = model.Name,
                Duration = model.Duration,
                Type = model.Type,
                //MonthlyFee = model.MonthlyFee,
            };
        }
    }
}
