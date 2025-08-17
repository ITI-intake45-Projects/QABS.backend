
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class TeacherAvailabilityConfiguration : IEntityTypeConfiguration<TeacherAvailability>
    {
        public void Configure(EntityTypeBuilder<TeacherAvailability> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DayOfWeek).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();

        }
    }
}
