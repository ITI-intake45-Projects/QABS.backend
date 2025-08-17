

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QABS.Models
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
           builder.HasKey(x => x.Id);

            builder.HasMany(sub => sub.Enrollments)
                .WithOne(en => en.SubscriptionPlan)
                .HasForeignKey(en => en.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
