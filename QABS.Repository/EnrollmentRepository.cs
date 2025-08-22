
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;

namespace QABS.Repository
{
    public class EnrollmentRepository : BaseRepository<Enrollment>
    {
        public EnrollmentRepository(QABSDbContext context) : base(context)
        {

        }

        public async Task<PaginationVM<EnrollmentDetailsVM>> SearchEnrollmentsByDate(
            DateTime? startdate = null,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(m => m.StartDate == startdate, m => m.StartDate, m => m.ToDetails(), false, pageSize, pageIndex);
            }
            catch
            {
                throw;
            }
        }
        public async Task<PaginationVM<EnrollmentDetailsVM>> SearchEnrollmentsByStudentId(
          string studentId,
          int pageSize = 10,
          int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    m => m.StudentId == studentId,
                    m => m.StartDate,
                    m => m.ToDetails(),
                    false,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaginationVM<EnrollmentDetailsVM>> SearchEnrollmentsByTeacherId(
            string teacherId,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    m => m.TeacherId == teacherId,
                    m => m.StartDate,
                    m => m.ToDetails(),
                    false,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaginationVM<EnrollmentDetailsVM>> GetEnrollmentsByStatusAsync(
            EnrollmentStatus status,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    m => m.Status == status,         // Filter by status
                    m => m.StartDate,                // Order by StartDate
                    m => m.ToDetails(),              // Project to Details VM
                    false,                           // Ascending order
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }

    }
}
