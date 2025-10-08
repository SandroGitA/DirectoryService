using DirectoryService.Core.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations
{
    public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
    {
        public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
        {
            builder.HasKey(dl => new { dl.DepartmentId, dl.LocationId });

            builder.HasOne(dl=>dl.Department)
                .WithMany(dl => dl.Locations)
                .HasForeignKey(dl => dl.DepartmentId);

            builder.HasOne(dl => dl.Location)
                .WithMany(dl => dl.Departments)
                .HasForeignKey(dl => dl.LocationId);
        }
    }
}
