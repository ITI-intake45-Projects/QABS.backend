

using QABS.Infrastructure;
using QABS.Models;

namespace QABS.Repository
{
    public class SubscribtionPlanRepository : BaseRepository<SubscribtionPlan>
    {
        public SubscribtionPlanRepository(QABSDbContext context) : base(context)
        {

        }


    }
}
