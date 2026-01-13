using CSharpFunctionalExtensions;
using DirectoryService.Core.Departments;
using Shared;

namespace DirectoryService.Core.Positions
{
    public sealed class Position
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
            UpdatedAt = CreatedAt;
        }

        public static Result<Position, Error> Create(PositionName name, string description, bool isActive)
        {
            if (description.Length >= MAX_LENGTH)
                return Error.Validation(null, $"Description must be less than {MAX_LENGTH} characters", nameof(description));

            return new Position(name, description, isActive);
        }
    }
}
