namespace DirectoryService.Core
{
    public record DepartmentName
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;

        public string Name { get; }

        private DepartmentName(string name)
        {
            Name = name;
        }

        public static DepartmentName Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                return null;
            }

            return new DepartmentName(name);
        }
    }
}
