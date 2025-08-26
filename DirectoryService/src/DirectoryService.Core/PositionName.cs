namespace DirectoryService.Core
{
    public record PositionName
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;

        public string Name { get; }

        private PositionName(string name)
        {
            this.Name = name;
        }

        public static PositionName Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                return null;
            }

            return new PositionName(name);
        }
    }
}
