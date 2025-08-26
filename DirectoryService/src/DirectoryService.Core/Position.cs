namespace DirectoryService.Core
{
    public class Position
    {
        public Guid Id { get; set; }

        public PositionName Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Department> Departments { get; set; } = [];

        public Position(string name, string? description, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = new PositionName(name);
            Description = description;
            IsActive = isActive;

            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
