
using Microsoft.EntityFrameworkCore;
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;



namespace QABS.Repository
{
    public class StudentPaymentRepository : BaseRepository<StudentPayment>
    {
        public StudentPaymentRepository(QABSDbContext context) : base(context)
        {
        }
        // Additional methods for StudentPaymentRepository can be added here

    

        public async Task<PaginationVM<StudentPaymentDetailsVM>> GetPaymentsByStudentIdAsync(
          string studentId,
          int pageSize = 10,
          int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                   m => m.StudentId == studentId,
                   m => m.PaymentDate,
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


        public async Task<StudentPaymentDetailsVM> GetStudentPaymentByEnrollmentId(int id)
        {
            try
            {
                
                return await GetList(p => p.EnrollmentId == id).Select(p => p.ToDetails()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
