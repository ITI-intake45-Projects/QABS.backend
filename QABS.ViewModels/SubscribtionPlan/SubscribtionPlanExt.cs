

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
                MonthlyFee = subscribtionPlanCreateVM.MonthlyFee,
                Name = subscribtionPlanCreateVM.Name,
                Type = subscribtionPlanCreateVM.Type,

            };
        }



        public static SubscribtionPlanDetailsVM ToDetails(this SubscribtionPlan model)
        {
            return new SubscribtionPlanDetailsVM
            {
                Id = model.Id,
                Name = model.Name,
                Duration = model.Duration,
                MonthlyFee = model.MonthlyFee,
                Type = model.Type,
            };
        }
    }
}
