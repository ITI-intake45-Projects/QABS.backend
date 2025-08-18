
using QABS.Models;

namespace QABS.ViewModels
{
    public static class PayoutItemExt
    {
        public static PayoutItem ToCreate(this PayoutItemCreateVM viewmodel)
        {
            return new PayoutItem
            {
                Amount = viewmodel.Amount,
                TeacherPayoutId = viewmodel.TeacherPayoutId,
                SessionId = viewmodel.SessionId
            };
        }

        public static PayoutItemDetailsVM ToDetails(this PayoutItem payoutItem)
        {
            return new PayoutItemDetailsVM
            {
                Id = payoutItem.Id,
                Amount = payoutItem.Amount,
                TeacherPayoutId = payoutItem.TeacherPayoutId,
                SessionDetails = payoutItem.Session.ToDetails()
            };
        }
    }
}
