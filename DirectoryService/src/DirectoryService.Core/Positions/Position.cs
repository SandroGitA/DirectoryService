using DirectoryService.Core.Departments;

namespace DirectoryService.Core.Positions
{
    public class Position
    {
        private const int MAX_LENGTH = 1000;

        public Guid Id { get; private set; }

        public PositionName Name { get; }

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyList<DepartmentPosition> Departments { get; private set; } = [];

        private Position() { }
        
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
            if (description.Length >= MAX_LENGTH)
            {
                throw new ArgumentException($"Description must be less than {MAX_LENGTH} characters");
            }

            return new Position(name, description, isActive);
        }
    }
}
