namespace DirectoryService.Core.Locations
{
    public record LocationName
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 120;

        public string Name { get; }

        private LocationName(string name)
        {
            Name = name;
        }

        public static LocationName Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                return null;
            }

            return new LocationName(name);
        }
    }
}
