using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimulationTask2.Models;

namespace SimulationTask2.DAL.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(20);

            builder.HasOne(p => p.Position)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.PositionId);

        }
    }
}
