

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.HasMany(st => st.Enrollments)
                .WithOne(en => en.Student)
                .HasForeignKey(en => en.StudentId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(t => t.User)
              .WithOne(u => u.Student)
              .HasForeignKey<Teacher>(t => t.UserId)
              .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(st => st.StudentPayments)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
