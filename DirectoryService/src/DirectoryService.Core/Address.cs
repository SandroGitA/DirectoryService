namespace DirectoryService.Core
{
    public record Address
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 100;

        public string Region { get; }

        public string City { get; }

        public string Street { get; }

        public int HouseNumber { get; }

        public int Room { get; }

        public int ZipCode { get; }


        private Address(string region, string city, string street, int houseNumber, int room, int zipCode)
        {
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Room = room;
            ZipCode = zipCode;
        }

        public static Address Create(string name, string city, string street, int houseNumber, int room, int zipCode)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                throw new ArgumentException("Name does not match the condition");
            }

            return new Address(name, city, street, houseNumber, room, zipCode);
        }
    }
}
