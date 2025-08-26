namespace DirectoryService.Core
{
    public class Location
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;

        public Guid Id { get; private set; }

        public LocationName Name { set; private get; }

        public string Address { get; private set; }

        public string Timezone { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Department> Departments { get; private set; } = [];

        private Location(LocationName name, string address, string timezone, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Timezone = timezone;
            IsActive = isActive;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Location Create(LocationName name, string address, string timezone, bool isActive)
        {
            if (name.Name.Length < MIN_LENGTH || name.Name.Length > MAX_LENGTH)
            {
                return null;
            }

            return new Location(name, address, timezone, isActive);
        }
    }
}
