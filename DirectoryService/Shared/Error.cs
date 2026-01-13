using System.Text.Json.Serialization;

namespace Shared
{
    public record Error
    {
        public string Code { get; }

        public string Message { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeError TypeError { get; }

        public string InvalidField { get; }

        public static Error None = new Error(string.Empty, string.Empty, TypeError.NONE, null);

        [JsonConstructor]
        private Error(string code, string message, TypeError typeError, string? invalidField = null)
        {
            Code = code;
            Message = message;
            TypeError = typeError;
            InvalidField = invalidField;
        }

        public static Error NotFound(string? code, string message, Guid? id)
        {
            return new Error(code ?? "Record not found", message, TypeError.NOT_FOUND);
        }

        public static Error Validation(string? code, string message, string? invalidField)
        {
            return new Error(code ?? "Value is invalid", message, TypeError.VALIDATION, invalidField);
        }

        public static Error Failure(string? code, string message)
        {
            return new Error(code ?? "Failure", message, TypeError.FAILURE);
        }

        public Errors ToErrors()
        {
            return this;
        }
    }
}
