namespace DirectoryService.Core
{
    public class Department
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;

        public Guid Id { get; private set; }

        public DepartmentName Name { get; }

        public string Identifier { get; private set; }

        public Guid? ParentId { get; private set; }

        public string Path { get; private set; }

        public short Depth { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Location> Locations { get; private set; }

        public List<Position> Positions { get; private set; } = [];

        private Department(DepartmentName name, string identifier, Guid? parentId, string path, short depth, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Identifier = identifier;
            ParentId = parentId ?? Guid.Empty;
            Path = path;
            Depth = depth;
            IsActive = isActive;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Department Create(DepartmentName name, string identifier, Guid? parentId, string path, short depth, bool isActive)
        {
            if (name.Name.Length < 3 || name.Name.Length > 150)
            {
                return null;
            }

            if (identifier.Length < 3 || identifier.Length > 150)
            {
                return null;
            }

            return new Department(name, identifier, parentId, path, depth, isActive);
        }
    }
}
