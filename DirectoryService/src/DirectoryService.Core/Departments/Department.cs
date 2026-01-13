using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Core.Departments
{
    public sealed class Department
    {
        public Guid Id { get; private set; }

        public DepartmentName Name { get; }

        public Identifier Identifier { get; }

        public Guid? ParentId { get; private set; }

        public Path Path { get; }

        public int Depth { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyList<Department> ChildrenDepartments { get; private set; } = [];
        
        public IReadOnlyList<DepartmentLocation> Locations { get; private set; } = [];

        public IReadOnlyList<DepartmentPosition> Positions { get; private set; } = [];

        private Department(){}

        private Department(
            Guid id,
            DepartmentName name,
            Identifier identifier,
            Guid? parentId,
            Path path,
            IEnumerable<DepartmentLocation> locations,
            int depth)
        {
            Id = id;
            Name = name;
            Identifier = identifier;
            ParentId = parentId ?? Guid.Empty;
            Path = path;
            Locations = locations.ToArray();
            Depth = depth;
            IsActive = true;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public static Result<Department, Error> CreateParent(
            Guid id,
            DepartmentName name, 
            Identifier identifier,
            IEnumerable<DepartmentLocation> locations)
        {
            var departmentLocations = locations.ToArray();
            if (departmentLocations.Count() <= 0)
                return Error.Validation(null, "Locations must have at least one location", nameof(locations));
            
            var parentPath = Path.CreateParent(name.Name);
            return new Department(id, name, identifier, null, parentPath.Value, departmentLocations, 0);
        }

        public static Result<Department, Error> CreateChild(
            Guid id,
            DepartmentName name, 
            Identifier identifier, 
            Department parent, 
            IEnumerable<DepartmentLocation>  locations)
        {
            var departmentLocations = locations.ToArray();
            if (departmentLocations.Count() <= 0)
                return Error.Validation(null, "Locations must have at least one location", nameof(departmentLocations));
            
            var childPath = parent.Path.CreateChild(name.Name);
            return new Department(id, name, identifier, parent.Id, childPath.Value,
                departmentLocations, parent.Depth + 1);
        }
    }
}
