using DirectoryService.Core.Departments;

namespace DirectoryService.Core.Locations
{
    public class Location
    {
        public Guid Id { get; private set; }

        public LocationName Name { get; }

        public Address Address { get; private set; }

        public Timezone Timezone { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyList<DepartmentLocation> Departments { get; private set; } = [];

        private Location() { }
        
        private Location(LocationName name, Address address, Timezone timezone, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Timezone = timezone;
            IsActive = isActive;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Location Create(LocationName name, Address address, Timezone timezone, bool isActive)
        {
            return new Location(name, address, timezone, isActive);
        }
    }
}
