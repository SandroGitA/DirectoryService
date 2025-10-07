using DirectoryService.Core;
using DirectoryService.Core.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("position");

            builder.HasKey(p => p.Id);

            builder.ComplexProperty(p => p.Name, pb =>
            {
                pb.Property(p => p.Name).HasMaxLength(LengthConstants.Length128).HasColumnName("name");
            });
        }
    }
}
