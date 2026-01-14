using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Core.Departments
{
    public sealed class Identifier
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;
        private const string RegEx = "[A-Za-z]";

        public string Name { get; }

        private Identifier(string name)
        {
            Name = name;
        }

        public static Result<Identifier, Error> Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
                return Error.Validation(null, "Name does not match the condition", nameof(name));

            bool isMatch = Regex.IsMatch(name, RegEx);

            if (!isMatch)
                return Error.Validation(null, "Name does not match the condition", nameof(name));

            return new Identifier(name);
        }
    }
}
