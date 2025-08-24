

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
                return await GetList().Select(t => t.ToDetails()).AsNoTracking().ToListAsync();
            }
            catch
            {
                throw;
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
