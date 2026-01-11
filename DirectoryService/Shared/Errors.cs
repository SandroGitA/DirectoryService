using System.Collections;

namespace Shared
{
    public class Errors : IEnumerable<Error>
    {
        private readonly List<Error> errors;

        public Errors(IEnumerable<Error> errors)
        {
            this.errors = [.. errors];
        }

        public IEnumerator<Error> GetEnumerator()
        {
            return errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator Errors(List<Error> errors) => new Errors(errors);

        public static implicit operator Errors(Error error) => new Errors([error]);
    }
}
