using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QABS.Models;

namespace QABS.Infrastructure
{
    public class QABSDbContext : IdentityDbContext<AppUser>
    {
        public QABSDbContext(DbContextOptions options) : base(options: options)
        {
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TeacherPayout> TeacherPayouts { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public DbSet<SubscribtionPlan> SubscribtionPlans { get; set; }



        //public DbSet<Level> Levels { get; set; }
        //public DbSet<PayoutItem> PayoutItems { get; set; }
        //public DbSet<Specialization> Specializations { get; set; }
        //public DbSet<EnrollmentLevelProgress> EnrollmentLevelProgresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // دي هتدور على كل IEntityTypeConfiguration<T> موجود في الـ Assembly وتطبقه
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QABSDbContext).Assembly);
        }
    }
}
