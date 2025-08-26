namespace DirectoryService.Core
{
    public class Department
    {
        public Guid Id { get; set; }

        public DepartmentName Name { get; set; }

        public string Identifier { get; set; }

        public Guid? ParentId { get; set; }

        public string Path { get; set; }

        public short Depth { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Location> Locations { get; set; }

        public List<Position> Positions { get; set; } = [];

        private Department(string name, string identifier, Guid? parentId, string path, short depth, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = new DepartmentName(name);
            Identifier = identifier;
            ParentId = parentId ?? Guid.Empty;
            Path = path;
            Depth = depth;
            IsActive = isActive;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Department Create(string name, string identifier, Guid? parentId, string path, short depth, bool isActive)
        {
            if (name.Length < 3 || name.Length > 150)
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
