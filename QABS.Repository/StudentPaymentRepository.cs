
using QABS.Infrastructure;
using QABS.Models;

namespace QABS.Repository
{
    public class StudentPaymentRepository : BaseRepository<StudentPayment>
    {
        public StudentPaymentRepository(QABSDbContext context) : base(context)
        {
        }
        // Additional methods for StudentPaymentRepository can be added here
    }
}
