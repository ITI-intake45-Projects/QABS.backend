

using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Data.Entity;

namespace QABS.Repository
{
    public class TeacherAvailabilityRepository : BaseRepository<TeacherAvailability>
    {
        public TeacherAvailabilityRepository(QABSDbContext context) : base(context)
        {
        }

        public async Task<List<TeacherAvailabilityDetailsVM>> GetAvailabilityByTeacherId(string teacherId)
        {
            try
            {
                return await GetList(t => t.TeacherId == teacherId).Select(t => t.ToDetails()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
