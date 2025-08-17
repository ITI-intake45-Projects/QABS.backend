
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;

namespace QABS.Models
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(en => en.Id);
           
            builder.Property(en => en.Status).HasDefaultValue(1);
            builder.Property(en => en.StartDate).IsRequired();
            builder.Property(en => en.EndDate).IsRequired();




            builder.HasMany(en => en.Sessions)
               .WithOne(en => en.Enrollment)
               .HasForeignKey(en => en.Enrollment.Id)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(en => en.StudentPayment)
                .WithOne(stpay => stpay.Enrollment)
                .HasForeignKey<StudentPayment>(stpay  => stpay.Enrollment.Id)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
