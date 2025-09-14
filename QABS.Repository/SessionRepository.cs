
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Linq.Expressions;

namespace QABS.Repository
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(QABSDbContext context) : base(context)
        {
        }

        // Additional methods for SessionRepository can be added here


        public async Task<PaginationVM<SessionEnrollmentDetailsVM>> SearchSessions(
            DateTime? startDate = null,
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1
)
        {
            try
            {
                Expression<Func<Session, bool>>? filter = null;

                if (startDate.HasValue)
                {
                    var targetDate = startDate.Value.Date;
                    filter = s => s.StartTime.HasValue && s.StartTime.Value.Date == targetDate;
                }

                return await SearchAsync(
                    filter,
                    s => s.StartTime,   // الترتيب حسب وقت البداية
                    s => s.ToEnrollmentDetails(),
                    descending,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }

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

        public async Task<List<Session>> GetCompletedSessionsByTeacherId(string teacherId)
        {
            try
            {
                return await GetList(s => s.Enrollment.TeacherId == teacherId && s.Status == SessionStatus.Completed).ToListAsync();
            }
            catch
            {
                throw;
            }
        }




    }
}
