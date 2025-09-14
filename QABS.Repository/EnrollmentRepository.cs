
using LinqKit;
using Microsoft.EntityFrameworkCore;
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Data.Entity;

namespace QABS.Repository
{
    public class EnrollmentRepository : BaseRepository<Enrollment>
    {
        public EnrollmentRepository(QABSDbContext context) : base(context)
        {

        }
        public async Task<PaginationVM<EnrollmentDetailsVM>> GetAllEnrollmentsAsync(
        int pageSize = 10,
        int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    null,                          // no filter (يعني هات الكل)
                    m => m.StartDate,              // order by StartDate
                    m => m.ToDetails(),            // projection
                    false,                         // ascending
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }


        public async Task<PaginationVM<EnrollmentListVM>> SearchEnrollmentList(
            string? studentId = "" ,
            string? teacherId = "",
            DateTime? startdate = null,
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {

                var predicate = PredicateBuilder.New<Enrollment>(true);

                if (!string.IsNullOrEmpty(studentId))
                    predicate = predicate.And(m => m.StudentId == studentId);

                if (!string.IsNullOrEmpty(teacherId))
                    predicate = predicate.And(m => m.TeacherId == teacherId);

                if (startdate.HasValue)
                    predicate = predicate.And(m => m.StartDate.Date == startdate.Value.Date);

                return await SearchAsync(predicate, m => m.StartDate, m => m.ToList(), false, pageSize, pageIndex);
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


        public async Task<List<StudentListVM>> GetEnrolledStudentsByTeacherIdAsync(string teacherId)
        {
            try
            {
                // Fetch data from DB (EF part)
                //var enrollments =  GetList(e => e.TeacherId == teacherId && e.Status == EnrollmentStatus.Active)
                var enrollments =  GetList(e => e.TeacherId == teacherId);

                ;

                // Map using your extension method (in-memory)
                //return await GetList(e => e.TeacherId == teacherId).Select(e => e.Student.ToList()).ToListAsync();
                //
                return enrollments.Select(e => e.Student.ToList()).ToList();
            }
            catch
            {
                throw;
            }
        }

    }
}
