using Shared;
using System.Text.Json.Serialization;

namespace DirectoryService.API.ResponseExtensions
{
    public record Envelope
    {
        public object? Result { get; }

        public Errors? Errors { get; }

        public bool IsError => Errors != null;

        public DateTime TimeGenerated { get; }

        [JsonConstructor]
        private Envelope(object? result, Errors? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result = null)
            => new(result, null);

        public static Envelope Fail(Errors errors)
            => new(null, errors);
    }

    public record Envelope<T>
    {
        public T? Result { get; }

        public Errors? Errors { get; }

        public bool IsError => Errors != null;

        public DateTime TimeGenerated { get; }

        private Envelope(T? result, Errors? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope<T> Ok(T? result = default)
            => new(result, null);

        public static Envelope<T> Fail(Error error)
            => new(default, error);
    }
}
