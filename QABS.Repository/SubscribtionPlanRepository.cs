

using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Data.Entity;

namespace QABS.Repository
{
    public class SubscribtionPlanRepository : BaseRepository<SubscribtionPlan>
    {
        public SubscribtionPlanRepository(QABSDbContext context) : base(context)
        {

        }

        public async Task<List<SubscribtionPlanDetailsVM>> GetSubscribtionPlans()
        {
            try
            {
                //return await GetList().Select(s => s.ToDetails()).ToListAsync();
                var list = GetList(); // هنا هيرجع List<SubscribtionPlan>

                return list.Select(t => t.ToDetails()).ToList(); // تحويل لـ ViewModel
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetSubscribtionPlans", ex);
            }
        }

        public async Task<bool> DoesNameExist(string name)
        {
            try
            {
                return await GetList().AnyAsync(t => t.Name == name);
            }
            catch
            {
                throw;
            }
        }

    }
}
