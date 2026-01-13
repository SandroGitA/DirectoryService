using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Core.Departments
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

        public static Result<DepartmentName, Error> Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
                return Error.Validation(null, "Name does not match the condition", nameof(name));

            return new DepartmentName(name);
        }
    }
}
