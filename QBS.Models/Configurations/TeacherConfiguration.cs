

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {

            builder.HasKey(s => s.UserId);
            builder.Property(t => t.HourlyRate).IsRequired();


            builder.HasMany(t => t.Enrollments)
                .WithOne(en => en.Teacher)
                .HasForeignKey(en => en.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(t => t.TeachersPayouts)
                .WithOne(tp => tp.Teacher)
                .HasForeignKey(tp => tp.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(t => t.TeacherAvailabilities)
                .WithOne(tA => tA.Teacher)
                .HasForeignKey(tA => tA.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(t => t.User)
                .WithOne(u => u.Teacher)
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
