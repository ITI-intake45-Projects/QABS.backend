

using LinqKit;
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using System.Linq.Expressions;

namespace QABS.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>
    {
        public TeacherRepository(QABSDbContext context) : base(context)
        {

        }

        public async Task<PaginationVM<TeacherDetailsVM>> SearchTeachers(
            string name = "",
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {

                // we dont need predicate builder if there is only 1 query.
                Expression<Func<Teacher, bool>>? filter = null;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    filter = r => r.User.FirstName.Contains(name) || r.User.LastName.Contains(name);
                }

                return await SearchAsync(filter,t => t.UserId, t => t.ToDetails(),false,pageSize,pageIndex);
            }
            catch
            {
                throw;
            }


        }
    }
}
