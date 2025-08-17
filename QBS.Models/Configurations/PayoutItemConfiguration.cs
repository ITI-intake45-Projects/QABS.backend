
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class PayoutItemConfiguration : IEntityTypeConfiguration<PayoutItem>
    {
        public void Configure(EntityTypeBuilder<PayoutItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).IsRequired();
            
        }
    }
}
