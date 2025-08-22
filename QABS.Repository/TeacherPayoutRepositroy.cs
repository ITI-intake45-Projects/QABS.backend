

using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;

namespace QABS.Repository
{
    public class TeacherPayoutRepositroy : BaseRepository<TeacherPayout>
    {
        public TeacherPayoutRepositroy(QABSDbContext context) : base(context)
        {
        }
        // Additional methods for TeacherPayoutRepository can be added here

        public async Task<PaginationVM<TeacherPayoutDetailsVM>> GetPayoutsByTeacherIdAsync(string teacherId , int pagesize = 10 , int pageindex = 1 )
        {
            try
            {
                return await SearchAsync(
                   m => m.TeacherId == teacherId,
                   m => m.PaidAt,
                   m => m.ToDetails(),
                   false,
                   pagesize,
                   pageindex


               );
            }
            catch
            {
                throw;
            }
        }


        




    
    }
}
