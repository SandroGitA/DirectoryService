using DirectoryService.Core.Locations;
using DirectoryService.Core.Positions;

namespace DirectoryService.Core.Departments
{
    public class Department
    {        
        public Guid Id { get; private set; }

        public DepartmentName Name { get; }

        public Identifier Identifier { get; }

        public Guid? ParentId { get; private set; }

        public Path Path { get; }

        public short Depth { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyList<Location> Locations { get; private set; } = [];

        public IReadOnlyList<Position> Positions { get; private set; } = [];

        private Department() { }        

        private Department(DepartmentName name, Identifier identifier,
            Guid? parentId, Path path, short depth, bool isActive)
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

        public static Department Create(DepartmentName name, Identifier identifier,
            Guid? parentId, Path path, short depth, bool isActive)
        {
            return new Department(name, identifier, parentId, path, depth, isActive);
        }
    }
}
