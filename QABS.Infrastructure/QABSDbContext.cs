using Microsoft.AspNetCore.Identity;
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

            // Apply all IEntityTypeConfiguration classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);

            // ==============================
            // 1. SEED SUBSCRIPTION PLANS
            // ==============================
            modelBuilder.Entity<SubscribtionPlan>().HasData(
            new SubscribtionPlan
            {
                Id = 1,
                Name = "٨ حصص (٤٠ دقيقة)",
                Type = SubscriptionType.EightSessions,
                Duration = SessionDurationType.FortyMinutes
            },
            new SubscribtionPlan
            {
                Id = 2,
                Name = "١٢ حصة (٤٠ دقيقة)",
                Type = SubscriptionType.TwelveSessions,
                Duration = SessionDurationType.FortyMinutes
            },
            new SubscribtionPlan
            {
                Id = 3,
                Name = "١٦ حصة (٤٠ دقيقة)",
                Type = SubscriptionType.SixteenSessions,
                Duration = SessionDurationType.FortyMinutes
            },
            new SubscribtionPlan
            {
                Id = 4,
                Name = "٨ حصص (٦٠ دقيقة)",
                Type = SubscriptionType.EightSessions,
                Duration = SessionDurationType.SixtyMinutes
            },
            new SubscribtionPlan
            {
                Id = 5,
                Name = "١٢ حصة (٦٠ دقيقة)",
                Type = SubscriptionType.TwelveSessions,
                Duration = SessionDurationType.SixtyMinutes
            },
            new SubscribtionPlan
            {
                Id = 6,
                Name = "١٦ حصة (٦٠ دقيقة)",
                Type = SubscriptionType.SixteenSessions,
                Duration = SessionDurationType.SixtyMinutes
            });

            // ==============================
            // 2. SEED ROLES (Admin)
            // ==============================
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "111",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "222",
                Name = "Teacher",
                NormalizedName = "TEACHER"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "333",
                Name = "Student",
                NormalizedName = "STUDENT"
            });

        }


    }
}
