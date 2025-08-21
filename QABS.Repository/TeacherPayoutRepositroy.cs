

using QABS.Infrastructure;
using QABS.Models;

namespace QABS.Repository
{
    public class TeacherPayoutRepositroy : BaseRepository<TeacherPayout>
    {
        public TeacherPayoutRepositroy(QABSDbContext context) : base(context)
        {
        }
        // Additional methods for TeacherPayoutRepository can be added here
    }
}
