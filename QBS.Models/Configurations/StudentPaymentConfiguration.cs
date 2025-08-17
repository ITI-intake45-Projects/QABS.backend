
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class StudentPaymentConfiguration : IEntityTypeConfiguration<StudentPayment>
    {
        public void Configure(EntityTypeBuilder<StudentPayment> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.PaymentDate)
                   .IsRequired();




       
        }
    }
}
