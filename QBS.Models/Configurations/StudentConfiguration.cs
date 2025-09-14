

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.UserId);


            builder.HasMany(st => st.Enrollments)
                .WithOne(en => en.Student)
                .HasForeignKey(en => en.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(t => t.User)
              .WithOne(u => u.Student)
              .HasForeignKey<Student>(t => t.UserId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(st => st.StudentPayments)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
