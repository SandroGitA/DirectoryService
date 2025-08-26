namespace DirectoryService.Core
{
    public class Location
    {
        public Guid Id { get; set; }

        public LocationName Name { set; get; }

        public string Address { get; set; }

        public string Timezone { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Department> Departments { get; set; } = [];

        private Location(string name, string address, string timezone, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = new LocationName(name);
            Address = address;
            Timezone = timezone;
            IsActive = isActive;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Location Create(string name, string address, string timezone, bool isActive)
        {
            return new Location(name, address, timezone, isActive);
        }
    }
}
