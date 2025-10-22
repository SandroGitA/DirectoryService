namespace DirectoryService.Contracts
{
    public record CreateLocationDto(string Name, Address Address, string Timezone);

    public record Address(string Name, string City, string Street, int HouseNumber, int Room, int ZipCode);
}
