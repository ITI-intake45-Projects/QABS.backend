

using QABS.Infrastructure;

namespace QABS.Repository
{
    public class UnitOfWork : IDisposable
    {
        public QABSDbContext _context { get; private set; }
        public TeacherRepository _teacherRepository { get; private set; }
        public AdminRepository _adminRepository { get; private set; }
        public TeacherPayoutRepositroy _teacherPayoutRepositroy { get; private set; }
        public StudentRepository _studentRepository { get; private set; }
        public StudentPaymentRepository _studentPaymentRepository { get; private set; }
        public EnrollmentRepository _enrollmentRepository { get; private set; }
        public SessionRepository _sessionRepository { get; private set; }
        public TeacherAvailabilityRepository _teacherAvailabilityRepository { get; private set; }
        public UserRepository _userRepository { get; private set; }
        public SubscribtionPlanRepository _subscribtionPlanRepository { get; private set; }

        public UnitOfWork(EnrollmentRepository enrollmentRepository,
             StudentRepository studentRepository,
             SessionRepository sessionRepository,
             UserRepository userRepository,
             TeacherRepository teacherRepository,
             TeacherPayoutRepositroy teacherPayoutRepositroy,
             TeacherAvailabilityRepository teacherAvailabilityRepository,
             StudentPaymentRepository studentPaymentRepository,
             SubscribtionPlanRepository subscribtionPlanRepository,
             AdminRepository adminRepository,
             QABSDbContext context
             )
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _teacherPayoutRepositroy = teacherPayoutRepositroy;
            _teacherAvailabilityRepository = teacherAvailabilityRepository;
            _studentPaymentRepository = studentPaymentRepository;
            _subscribtionPlanRepository = subscribtionPlanRepository;
            _adminRepository = adminRepository;
            _context = context;
        }



        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
