
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Collections.Immutable;
using System.Data.Entity;

namespace QABS.Repository
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(QABSDbContext context) : base(context)
        {
        }

        // Additional methods for SessionRepository can be added here
        public async Task<List<SessionDetailsVM>> GetSessionsByEnrollmentIdAsync(int enrollmentId)
        {
            try
            {
                return await GetList(s => s.EnrollmentId == enrollmentId).Select(s => s.ToDetails()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SessionDetailsVM>> GetSessionsByTeacherPayoutIdAsync(int teacherPayoutId)
        {
            try
            {
                return await GetList(s => s.TeacherPayoutId == teacherPayoutId).Select(s => s.ToDetails()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }




    }
}
