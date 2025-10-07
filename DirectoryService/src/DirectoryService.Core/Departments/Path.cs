namespace DirectoryService.Core.Departments
{
    public record Path
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;

        public string Name { get; set; }

        private Path(string name)
        {
            Name = name;
        }

        public static Path Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                throw new ArgumentException("Name does not match the condition");
            }

            return new Path(name);
        }
    }
}
