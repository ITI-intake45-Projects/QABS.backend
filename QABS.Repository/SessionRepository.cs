
using QABS.Infrastructure;
using QABS.Models;

namespace QABS.Repository
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(QABSDbContext context) : base(context)
        {
        }
    }
}
