using System.Text.RegularExpressions;

namespace DirectoryService.Core.Departments
{
    public class Identifier
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 150;
        private const string RegEx = "[A-Za-z]";

        public string Name { get; }

        private Identifier(string name)
        {
            Name = name;
        }

        public static Identifier Create(string name)
        {
            if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            {
                throw new ArgumentException("Name does not match the condition");
            }

            bool isMatch = Regex.IsMatch(name, RegEx);

            if (!isMatch)
            {
                throw new ArgumentException("Доступна только латиница");
            }

            return new Identifier(name);
        }
    }
}
