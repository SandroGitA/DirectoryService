using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Core.Departments
{
    public record Path
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;
        
        private const char SEPARATOR = '/';

        public string Name { get; set; }

        private Path(string name)
        {
            Name = name;
        }
        
        public static Result<Path, Error> CreateParent(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
                return Error.Validation(null, "Name does not match the condition", nameof(name));

            return new Path(name);
        }

        public Result<Path, Error> CreateChild(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
                return Error.Validation(null, "Name does not match the condition", nameof(name));
            
            return new Path(nameof(name) + SEPARATOR + name);
        }
    }
}
