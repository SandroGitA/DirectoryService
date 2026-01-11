using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Core.Locations
{
    public record Timezone
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 30;

        public string Iana { get; }

        private Timezone(string iana)
        {
            Iana = iana;
        }

        public static Result<Timezone, Error> Create(string iana)
        {
            if (iana.Length < MIN_LENGTH || iana.Length > MAX_LENGTH)
            {
                return Error.Validation(null, "Name does not match the condition", null);
            }

            return new Timezone(iana);
        }
    }
}
