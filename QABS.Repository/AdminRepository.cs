using QABS.Infrastructure;
using QABS.Models;

namespace QABS.Repository
{
    public class AdminRepository : BaseRepository<Admin>
    {
        public AdminRepository(QABSDbContext context) : base(context)
        {

        }
    }
}
