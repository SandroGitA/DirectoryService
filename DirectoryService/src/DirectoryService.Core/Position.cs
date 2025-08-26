namespace DirectoryService.Core
{
    public class Position
    {
        public Guid Id { get; private set; }

        public PositionName Name { get; private set; }

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Department> Departments { get; private set; } = [];

        private Position(PositionName name, string? description, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = isActive;

            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public static Position Create(PositionName name, string description, bool isActive)
        {
            if (name.Name.Length < 3 || name.Name.Length > 150)
            {
                return null;
            }

            return new Position(name, description, isActive);
        }
    }
}
