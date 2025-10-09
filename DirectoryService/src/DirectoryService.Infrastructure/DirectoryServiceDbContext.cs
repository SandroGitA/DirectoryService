using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Core.Positions;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure
{
    public class DirectoryServiceDbContext : DbContext
    {
        public DirectoryServiceDbContext(DbContextOptions<DirectoryServiceDbContext> options) : base(options) { }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirectoryServiceDbContext).Assembly);
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }

        public DbSet<DepartmentPosition> DepartmentPositions { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Position> Positions { get; set; }        
    }
}
