
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class TeacherPayoutConfiguration : IEntityTypeConfiguration<TeacherPayout>
    {
        public void Configure(EntityTypeBuilder<TeacherPayout> builder)
        {
            builder.HasKey(tp => tp.Id);
            builder.Property(tp => tp.PaidAt).IsRequired();
            
            builder.Property(tp => tp.ImageUrl).HasColumnType("NVARCHAR(MAX)");

            builder.HasMany(tp => tp.PayoutItems)
                .WithOne(p => p.TeacherPayout)
                .HasForeignKey(p => p.TeacherPayoutId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
