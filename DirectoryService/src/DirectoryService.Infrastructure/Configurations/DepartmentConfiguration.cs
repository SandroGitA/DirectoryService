using DirectoryService.Core;
using DirectoryService.Core.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");

            builder.HasKey(d => d.Id).HasName("id");

            builder.ComplexProperty(d => d.Name, db =>
            {
                db.Property(d => d.Name)
                    .HasMaxLength(LengthConstants.Length128)
                    .HasColumnName("name");
            });

            builder.ComplexProperty(d => d.Identifier, db =>
            {
                db.Property(d => d.Name)
                    .HasMaxLength(LengthConstants.Length128)
                    .HasColumnName("name");
            });

            builder.ComplexProperty(d => d.Path, db =>
            {
                db.Property(d => d.Name)
                    .HasMaxLength(LengthConstants.Length128)
                    .HasColumnName("name");
            });

            builder.HasMany(d => d.Locations)
                .WithOne()
                .HasForeignKey(d=> d.DepartmentId);
            
            builder.HasMany(d => d.Positions)
                .WithOne()
                .HasForeignKey(d => d.DepartmentId);
            
            builder.HasMany(d=>d.ChildrenDepartments)
                .WithOne()
                .IsRequired(false)
                .HasForeignKey(d=>d.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
