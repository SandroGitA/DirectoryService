using DirectoryService.Core;
using DirectoryService.Core.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(l => l.Id);

            builder.ComplexProperty(l => l.Name, lb =>
            {
                lb.Property(l => l.Name)
                    .HasMaxLength(LengthConstants.Length128)
                    .HasColumnName("name");
            });

            builder.ComplexProperty(l => l.Address, lb =>
            {
                lb.Property(l => l.Region).HasMaxLength(LengthConstants.Length128).HasColumnName("region");
                lb.Property(l => l.City).HasMaxLength(LengthConstants.Length32).HasColumnName("city");
                lb.Property(l => l.Street).HasMaxLength(LengthConstants.Length64).HasColumnName("street");
                lb.Property(l => l.HouseNumber).HasMaxLength(LengthConstants.Length16).HasColumnName("houseNumber");
                lb.Property(l => l.Room).HasMaxLength(LengthConstants.Length16).HasColumnName("room");
                lb.Property(l => l.ZipCode).HasMaxLength(LengthConstants.Length16).HasColumnName("zipCode");
            });

            builder.ComplexProperty(l => l.Timezone, lb =>
            {
                lb.Property(l => l.Iana).HasMaxLength(LengthConstants.Length128).HasColumnName("iana");
            });
        }
    }
}
